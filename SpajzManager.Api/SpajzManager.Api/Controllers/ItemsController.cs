using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Entities;
using SpajzManager.Api.Models;
using SpajzManager.Api.Services;

namespace SpajzManager.Api.Controllers
{
    [Route("api/households/{householdId}/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IMailService _mailService;
        private readonly ISpajzManagerRepository _spajzManagerRepository;
        private readonly IMapper _mapper;
        private const int maxItemsPageSize = 20;

        public ItemsController(ILogger<ItemsController> logger,
            IMailService mailService, 
            ISpajzManagerRepository spajzManagerRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ??
                throw new ArgumentNullException(nameof(mailService));
            _spajzManagerRepository = spajzManagerRepository ??
                throw new ArgumentNullException(nameof(spajzManagerRepository));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems(int householdId,
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                _logger.LogInformation(
                    $"Household with id {householdId} wasn't found when accessing items.");
                return NotFound();
            }

            if (pageSize > maxItemsPageSize)
            {
                pageSize = maxItemsPageSize;
            }

            var items = await _spajzManagerRepository
                .GetItemsForHouseholdAsync(householdId, name,
                    searchQuery, pageNumber, pageSize);

            return Ok(_mapper.Map<IEnumerable<ItemDto>>(items));            
        }

        [HttpGet("{itemid}", Name = "GetItem")]
        public async Task<ActionResult<ItemDto>> GetItem(
            int householdId, int itemId) 
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                _logger.LogInformation(
                    $"Household with id {householdId} wasn't found when accessing items.");
                return NotFound();
            }

            var item = await _spajzManagerRepository
                .GetItemForHouseholdAsync(householdId, itemId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ItemDto>(item));
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(
            int householdId, 
            ItemForCreationDto item)
        {            
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var finalItem = _mapper.Map<Entities.Item>(item);

            await _spajzManagerRepository.AddItemForHouseholdAsync(
                householdId, finalItem);

            await _spajzManagerRepository.SaveChangesAsync();

            var createItemToReturn = 
                _mapper.Map<Models.ItemDto>(finalItem);

            return CreatedAtRoute("GetItem",
                new
                {
                    householdId = householdId,
                    itemId = createItemToReturn.Id
                },
                createItemToReturn);
        }

        [HttpPut("{itemid}")]
        public async Task<ActionResult<ItemDto>> UpdateItem(
            int householdId, int itemId,
            ItemForUpdateDto item)
        {            
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var itemEntity = await _spajzManagerRepository
                .GetItemForHouseholdAsync(householdId, itemId);
            if (itemEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(item, itemEntity);

            await _spajzManagerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{itemid}")]
        public async Task<ActionResult> PartiallyUpdateItem(
            int householdId, int itemId,
            JsonPatchDocument<ItemForUpdateDto> patchDocument)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var itemEntity = await _spajzManagerRepository
                .GetItemForHouseholdAsync(householdId, itemId);
            if (itemEntity == null)
            {
                return NotFound();
            }

            var itemToPatch = _mapper.Map<ItemForUpdateDto>(itemEntity);

            patchDocument.ApplyTo(itemToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(itemToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(itemToPatch, itemEntity);

            await _spajzManagerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{itemid}")]
        public async Task<ActionResult> DeleteItem(
            int householdId, int itemId)
        {
            if (!await _spajzManagerRepository.HouseholdExistsAsync(householdId))
            {
                return NotFound();
            }

            var itemEntity = await _spajzManagerRepository
                .GetItemForHouseholdAsync(householdId, itemId);
            if (itemEntity == null)
            {
                return NotFound();
            }

            _spajzManagerRepository.DeleteItem(itemEntity);

            await _spajzManagerRepository.SaveChangesAsync();

            _mailService.Send("Item deleted.",
                $"Item {itemEntity.Name} with id {itemEntity.Id} was deleted.");

            return NoContent();
        }
    }
}
