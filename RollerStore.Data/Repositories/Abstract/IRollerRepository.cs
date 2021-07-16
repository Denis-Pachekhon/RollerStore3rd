using RollerStore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Data.Repositories.Abstract
{
    public interface IRollerRepository
    {
        Task<List<RollerEntity>> GetAsync(int storeId);
        Task<RollerEntity> GetByIdAsync(int id);
        Task<RollerEntity> UpdatePriceAsync(RollerEntity roller);
        Task RemoveAsync(int id);
        Task<RollerEntity> AddAsync(RollerEntity roller);

        bool IsDeleted(int id);
    }
}
