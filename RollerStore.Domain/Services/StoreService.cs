using AutoMapper;
using RollerStore.Data.Entities;
using RollerStore.Data.Repositories.Abstract;
using RollerStore.Domain.Services.Abstract;
using RollerStore.Domain.Services.Exception;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Domain.Services
{
    public class StoreService : IStoreService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;

        public StoreService(
            IMapper mapper,
            IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
        }
        public async Task<Store> AddAsync(Store store)
        {
            var storeEntity = await _storeRepository.AddAsync(_mapper.Map<StoreEntity>(store));

            return _mapper.Map<Store>(storeEntity);   
        }   

        public async Task<List<Store>> GetAsync()
        {
            var storeEntities =  await _storeRepository.GetAsync();

            return _mapper.Map<List<Store>>(storeEntities);
        }

        public async Task<Store> GetByIdAsync(int id)
        {
            if (id <= 0 || IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }
            
            var storeEntiry = await _storeRepository.GetByIdAsync(id);

            return _mapper.Map<Store>(storeEntiry);
        }

        public async Task RemoveAsync(int id)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var storeEntity = await _storeRepository.GetByIdAsync(id);

            if (storeEntity == null)
            {
                throw new ValidationException($"Store not found, storeId = {id}");
            }

            await _storeRepository.RemoveAsync(storeEntity.Id);
        }

        public async Task<Store> UpdateNameAsync(int id, string name)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var storeEntity = await _storeRepository.GetByIdAsync(id);

            if (storeEntity == null)
            {
                throw new ValidationException($"Store not found, storeId = {id}");
            }

            if (name == null)
            {
                throw new ValidationException("Invalid store name");
            }

            storeEntity.Name = name;

            await _storeRepository.UpdateNameAsync(storeEntity);

            return _mapper.Map<Store>(storeEntity);
        }

        private bool IsDeleted(int id)
        {
            return _storeRepository.IsDeleted(id);
        }
    }
}
