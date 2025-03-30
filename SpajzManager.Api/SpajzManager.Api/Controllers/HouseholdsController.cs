using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Entities;
using SpajzManager.Api.Models;
using SpajzManager.Api.Services;


//todo: azt kéne hogy le lehessen kérdezni egy householdnak milyen storage-ei vannak. lehessen postolni. a

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

        [HttpGet("{householdid}/storages", Name = "GetStorages")]
        public async Task<ActionResult<IEnumerable<StorageDto>>> GetStorages(
            int householdId)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var storages = await _spajzManagerRepository
                .GetStoragesForHouseholdAsync(householdId);

            if (storages == null || !storages.Any())
            {
                return NotFound();
            }

            var storagesToReturn = _mapper.Map<IEnumerable<StorageDto>>(storages);

            return Ok(storagesToReturn);
        }

        [HttpGet("{householdid}/storages/{storageid}", Name = "GetStorage")]
        public async Task<ActionResult<StorageDto>> GetStorage(
            int householdId,
            int storageId)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var storage = await _spajzManagerRepository
                .GetStorageForHouseholdAsync(householdId, storageId);

            if (storage == null)
            {
                return NotFound();
            }

            var storageToReturn = _mapper.Map<StorageDto>(storage);

            return Ok(storageToReturn);
        }

        [HttpPost("{id}/storages")]
        public async Task<ActionResult<StorageDto>> CreateStorage(
            int id,
            StorageForCreationDto storageDto)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(id))
            {
                return NotFound();
            }

            var newStorage = _mapper.Map<Storage>(storageDto);
            newStorage.HouseholdId = id;

            await _spajzManagerRepository
                .AddStorageForHouseholdAsync(id, newStorage);
            await _spajzManagerRepository
                .SaveChangesAsync();

            var createdStorage = _mapper.Map<StorageDto>(newStorage);

            return CreatedAtRoute("GetStorage",
                new { householdid = id,
                      storageid = createdStorage.Id},
                createdStorage);        
        }
    }
}

