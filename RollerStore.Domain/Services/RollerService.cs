using AutoMapper;
using RollerStore.Data.Entities;
using RollerStore.Data.Repositories.Abstract;
using RollerStore.Domain.Services.Abstract;
using RollerStore.Domain.Services.Exception;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore.Domain.Services
{
    public class RollerService : IRollerService
    {
        private readonly IMapper _mapper;
        private readonly IRollerRepository _rollerRepository;

        public RollerService(
            IMapper mapper,
            IRollerRepository rollerRepository)
        {
            _mapper = mapper;
            _rollerRepository = rollerRepository;
        }
        public async Task<Roller> AddAsync(Roller roller)
        {
            var rollerEntity = await _rollerRepository.AddAsync(_mapper.Map<RollerEntity>(roller));

            return _mapper.Map<Roller>(rollerEntity);
        }

        public async Task<List<Roller>> GetAsync(int storeId)
        {
            var rollerEntities = await _rollerRepository.GetAsync(storeId);

            return _mapper.Map<List<Roller>>(rollerEntities);
        }

        public async Task<Roller> GetByIdAsync(int id)
        {
            if (id <= 0 || IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var rollerEntiry = await _rollerRepository.GetByIdAsync(id);

            return _mapper.Map<Roller>(rollerEntiry);
        }

        public async Task RemoveAsync(int id)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var rollerEntity = await _rollerRepository.GetByIdAsync(id);

            if (rollerEntity == null)
            {
                throw new ValidationException($"Roller not found, rollerId = {id}");
            }

            await _rollerRepository.RemoveAsync(rollerEntity.Id);
        }

        public async Task<Roller> UpdatePriceAsync(int id, double price)
        {
            if (IsDeleted(id))
            {
                throw new ValidationException("Bad id");
            }

            var rollerEntity = await _rollerRepository.GetByIdAsync(id);

            if (rollerEntity == null)
            {
                throw new ValidationException($"Roller not found, rollerId = {id}");
            }

            rollerEntity.Price = price;

            await _rollerRepository.UpdatePriceAsync(rollerEntity);

            return _mapper.Map<Roller>(rollerEntity);
        }

        private bool IsDeleted(int id)
        {
            return _rollerRepository.IsDeleted(id);
        }
    }
}
