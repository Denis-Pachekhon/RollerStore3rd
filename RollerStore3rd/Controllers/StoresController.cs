using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RollerStore.Domain.Services.Abstract;
using RollerStore3rd.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RollerStore3rd.Controllers
{
    [ApiController]
    [Route("api/RollerStore/v1")]
    public class StoresController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStoreService _storeService;

        public StoresController(
            IMapper mapper,
            IStoreService storeService)
        {
            _mapper = mapper;
            _storeService = storeService;
        }

        [HttpGet("Stores")]
        public async Task<IActionResult> GetAsync()
        {
            var stores = await _storeService.GetAsync();

            return Ok(_mapper.Map<List<Store>>(stores));
        }

        [HttpGet("Stores/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var store = await _storeService.GetByIdAsync(id);

            return Ok(_mapper.Map<Store>(store));
        }

        [HttpPost("Stores")]
        public async Task<IActionResult> PostAsync(CreateStore store)
        {
            var storeDomain = _mapper.Map<RollerStore.Domain.Store>(store);

            var createdStore = await _storeService.AddAsync(storeDomain);

            return Ok(_mapper.Map<Store>(createdStore));
        }

        [HttpDelete("Stores/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _storeService.RemoveAsync(id);

            return Ok();
        }

        [HttpPatch("Stores/{id}")]
        public async Task<IActionResult> UpdateNameAsync(int storeId, UpdateNameStore storeName)
        {
            var store = await _storeService.UpdateNameAsync(storeId, storeName.Name);

            return Ok(_mapper.Map<Store>(store));
        }
    }
}
