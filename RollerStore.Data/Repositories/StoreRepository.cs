using Microsoft.EntityFrameworkCore;
using RollerStore.Data.DB;
using RollerStore.Data.Entities;
using RollerStore.Data.Repositories.Abstract;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly RollerStoreDbContext _context;

        public StoreRepository(RollerStoreDbContext context)
        {
            _context = context;
        }

        public async Task<StoreEntity> AddAsync(StoreEntity store)
        {
            var result = await _context.AddAsync(store);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<StoreEntity>> GetAsync()
        {
            var entities = await _context.Stores
                .Where(s => s.IsDeleted == false)
                .ToListAsync();

            return entities;
        }

        public async Task<StoreEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);

            return entity;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id);
            
            foreach(var roller in entity.Rollers)
            {
                roller.IsDeleted = true;
            }

            entity.IsDeleted = true;

            _context.Stores.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<StoreEntity> UpdateNameAsync(StoreEntity store)
        {
            var result = _context.Update(store);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public bool IsDeleted(int id)
        {
            return _context.Stores.Count(s => s.Id == id && s.IsDeleted == true) > 0;
        }
    }
}
