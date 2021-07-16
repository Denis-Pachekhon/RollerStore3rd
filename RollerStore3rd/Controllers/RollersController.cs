using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RollerStore.Domain.Services.Abstract;
using RollerStore3rd.Contracts;

namespace RollerStore3rd.Controllers
{
    [Route("api/RollerStore/v1")]
    [ApiController]
    public class RollersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRollerService _rollerService;

        public RollersController(
            IMapper mapper,
            IRollerService rollerService)
        {
            _mapper = mapper;
            _rollerService = rollerService;
        }

        [HttpGet("Stores/{storeId}/Rollers")]
        public async Task<IActionResult> GetAsync(int storeId)
        {
            var rollers = await _rollerService.GetAsync(storeId);

            return Ok(_mapper.Map<List<Roller>>(rollers));
        }

        [HttpGet("Rollers/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var roller = await _rollerService.GetByIdAsync(id);

            return Ok(_mapper.Map<Roller>(roller));
        }

        [HttpPost("Stores/{storeId}/Rollers")]
        public async Task<IActionResult> PostAsync(CreateRoller roller, int storeId)
        {
            var rollerDomain = _mapper.Map<RollerStore.Domain.Roller>(roller);

            rollerDomain.StoreId = storeId;

            var createdRoller = await _rollerService.AddAsync(rollerDomain);

            return Ok(_mapper.Map<Roller>(createdRoller));
        }

        [HttpDelete("Rollers/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _rollerService.RemoveAsync(id);

            return Ok();
        }

        [HttpPatch("Rollers/{id}")]
        public async Task<IActionResult> UpdateNameAsync(int rollerId, UpdateRollerPrice rollerPrice)
        {
            var roller = await _rollerService.UpdatePriceAsync(rollerId, rollerPrice.Price);

            return Ok(_mapper.Map<Roller>(roller));
        }
    }
}
