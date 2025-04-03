using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Entities;
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

        [HttpGet("{householdid}", Name = "GetHousehold")]
        public async Task<IActionResult> GetHousehold(
            int householdId,
            bool includeItems = false,
            bool includeStorages = false)
        {
            var household = await _spajzManagerRepository.GetHouseholdAsync(householdId, includeItems, includeStorages);
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

        [HttpPost]
        public async Task<ActionResult<HouseholdDto>> CreateHousehold(
            HouseholdForCreationDto householdDto)
        {
            if (await _spajzManagerRepository.HouseholdExistsAsync(householdDto.Name))
            {
                return Conflict("Household with this name already exists");
            }
            
            var newHousehold = _mapper.Map<Household>(householdDto);

            await _spajzManagerRepository.AddHouseholdAsync(newHousehold);

            await _spajzManagerRepository.SaveChangesAsync();

            var createdHousehold = _mapper.Map<HouseholdDto>(newHousehold);

            return CreatedAtRoute("GetHousehold",
                new
                {
                    householdid = createdHousehold.Id,
                },
                createdHousehold);
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

        [HttpPut("{householdid}/storages/{storageid}")]
        public async Task<ActionResult<StorageDto>> UpdateStorage(
            int householdId,
            int storageId,
            StorageForUpdateDto storage)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var storageEntity = await _spajzManagerRepository
                .GetStorageForHouseholdAsync(householdId, storageId);

            if (storageEntity == null || 
                storageEntity.HouseholdId != householdId)
            {
                return NotFound();
            }

            _mapper.Map(storage, storageEntity);

            await _spajzManagerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{householdid}/storages/{storageid}")]
        public async Task<ActionResult> PartiallyUpdateStorage(
            int householdId,
            int storageId,
            JsonPatchDocument<StorageForUpdateDto> patchDocument)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var storageEntity = await _spajzManagerRepository
                .GetStorageForHouseholdAsync(householdId, storageId);

            if (storageEntity == null ||
                storageEntity.HouseholdId != householdId)
            {
                return NotFound();
            }

            var storageToPath = _mapper.Map<StorageForUpdateDto>(storageEntity);

            patchDocument.ApplyTo(storageToPath, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(storageToPath))
            {
                return BadRequest();
            }

            _mapper.Map(storageToPath, storageEntity);

            await _spajzManagerRepository.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{householdid}/storages/{storageid}")]
        public async Task<ActionResult> DeleteStorage(
            int householdId,
            int storageId)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var storageEntity = await _spajzManagerRepository
                .GetStorageForHouseholdAsync(householdId, storageId);

            if (storageEntity == null ||
                storageEntity.HouseholdId != householdId)
            {
                return NotFound();
            }

            var isStorageEmpty = await _spajzManagerRepository
                .IsStorageEmptyAsync(householdId, storageId);

            if (!isStorageEmpty)
            {
                return BadRequest("This storage is not empty. Please move all items before deletion.");
            }

            _spajzManagerRepository.DeleteStorate(storageEntity);

            await _spajzManagerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{householdid}")]
        public async Task<ActionResult> DeleteHousehold(
            int householdId)
        {
            var household = await _spajzManagerRepository
                .GetHouseholdAsync(householdId, true, true);

            if (household == null)
            {
                return NotFound();
            }

            foreach (var item in household.Items)
            {
                _spajzManagerRepository.DeleteItem(item);
            }


            foreach (var storage in household.Storages)
            {
                _spajzManagerRepository.DeleteStorate(storage);
            }

            _spajzManagerRepository.DeleteHousehold(household);
            await _spajzManagerRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}

