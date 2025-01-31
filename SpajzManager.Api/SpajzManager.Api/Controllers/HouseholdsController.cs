using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Models;
using SpajzManager.Api.Services;

namespace SpajzManager.Api.Controllers
{
    [ApiController]
    [Route("api/households")]
    public class HouseholdsController : ControllerBase
    {
        private readonly ISpajzManagerRepository _spajzManagerRepository;
        private readonly IMapper _mapper;

        public HouseholdsController(ISpajzManagerRepository spajzManagerRepository,
            IMapper mapper) 
        {
            _spajzManagerRepository = spajzManagerRepository ?? 
                throw new ArgumentNullException(nameof(spajzManagerRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseholdWithoutItemsDto>>> GetHouseholds()
        {
            var householdEntities = await _spajzManagerRepository.GetHouseholdsAsync();
            return Ok(_mapper.Map<IEnumerable<HouseholdWithoutItemsDto>>(householdEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHousehold(
            int id, bool includeItems = false)
        {
            var household = await _spajzManagerRepository.GetHouseholdAsync(id, includeItems);
            if (household == null)
            {
                return NotFound();
            }

            if (includeItems)
            {
                return Ok(_mapper.Map<HouseholdDto>(household));
            }

            return Ok(_mapper.Map<HouseholdWithoutItemsDto>(household));
        }
    }
}
