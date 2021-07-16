using Microsoft.EntityFrameworkCore;
using RollerStore.Data.DB;
using RollerStore.Data.Entities;
using RollerStore.Data.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RollerStore.Data.Repositories
{
    public class RollerRepository : IRollerRepository
    {
        private readonly RollerStoreDbContext _context;

        public RollerRepository(RollerStoreDbContext context)
        {
            _context = context;
        }

        public async Task<RollerEntity> AddAsync(RollerEntity roller)
        {
            var result = await _context.Rollers.AddAsync(roller);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<RollerEntity>> GetAsync(int storeId)
        {
            var entities = await _context.Rollers
                .Where(r => r.StoreId == storeId && r.IsDeleted == false)
                .ToListAsync();

            return entities;
        }

        public async Task<RollerEntity> GetByIdAsync(int id)
        {
            var entity = await _context.Rollers.FirstOrDefaultAsync(s => s.Id == id);

            return entity;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Rollers.FirstOrDefaultAsync(s => s.Id == id);

            entity.IsDeleted = true;

            _context.Rollers.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<RollerEntity> UpdatePriceAsync(RollerEntity roller)
        {
            var result = _context.Update(roller);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public bool IsDeleted(int id)
        {
            return _context.Rollers.Count(s => s.Id == id && s.IsDeleted == true) > 0;
        }
    }
}
