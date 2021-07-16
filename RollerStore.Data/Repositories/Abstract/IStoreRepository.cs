using RollerStore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Data.Repositories.Abstract
{
    public interface IStoreRepository
    {
        Task<List<StoreEntity>> GetAsync();
        Task<StoreEntity> GetByIdAsync(int id);
        Task<StoreEntity> UpdateNameAsync(StoreEntity store);
        Task RemoveAsync(int id);
        Task<StoreEntity> AddAsync(StoreEntity store);

        bool IsDeleted(int id);
    }
}
