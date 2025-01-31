using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        private readonly HouseholdDataStore _householdDataStore;

        public ItemsController(ILogger<ItemsController> logger,
            IMailService mailService,
            HouseholdDataStore householdDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _householdDataStore = householdDataStore ?? throw new ArgumentException(nameof(householdDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetItems(int householdId)
        {
            try
            {                
                var household = _householdDataStore.Households
                .FirstOrDefault(h => h.Id == householdId);

                if (household == null)
                {
                    return NotFound();
                }

                return Ok(household.Items);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(
                    $"Exception while getting items for household with id {householdId}.",
                    ex);
                return StatusCode(500,
                    "A problem happened while handling your request.");
            }
            
        }

        [HttpGet("{itemid}", Name = "GetItem")]
        public ActionResult<ItemDto> GetItem(
            int householdId, int itemId) 
        {
            var household = _householdDataStore.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
                _logger.LogInformation($"Household with id {householdId} wasn't found when accessing items.");
                return NotFound();
            }

            var item = household.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(
            int householdId, 
            ItemForCreationDto item)
        {
            var household = _householdDataStore.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
                return NotFound();
            }

            // temporary - to be improved
            var maxItemId = _householdDataStore.Households
                .SelectMany(h => h.Items).Max(i => i.Id);

            var finalItem = new ItemDto()
            {
                Id = ++maxItemId,
                Name = item.Name,
                Description = item.Description,
            };

            household.Items.Add(finalItem);

            return CreatedAtRoute("GetItem",
                new
                {
                    householdId,
                    itemId = finalItem.Id
                },
                finalItem);
        }

        [HttpPut("{itemid}")]
        public ActionResult<ItemDto> UpdateItem(
            int householdId, int itemId,
            ItemForUpdateDto item)
        {
            var household = _householdDataStore.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null) 
            {
                return NotFound(); 
            }

            var itemFromStore = household.Items
                .FirstOrDefault(i => i.Id == itemId);
            if (itemFromStore == null)
            {
                return NotFound();
            }

            itemFromStore.Name = item.Name;
            itemFromStore.Description = item.Description;

            return NoContent();
        }

        [HttpPatch("{itemid}")]
        public ActionResult PartiallyUpdateItem(
            int householdId, int itemId,
            JsonPatchDocument<ItemForUpdateDto> patchDocument)
        {
            var household = _householdDataStore.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
                return NotFound();
            }

            var itemFromStore = household.Items
                .FirstOrDefault(i => i.Id == itemId);
            if (itemFromStore == null)
            {
                return NotFound();
            }

            var itemToPatch =
                new ItemForUpdateDto()
                {
                    Name = itemFromStore.Name,
                    Description = itemFromStore.Description
                };

            patchDocument.ApplyTo(itemToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(itemToPatch))
            {
                return BadRequest();
            }

            itemFromStore.Name = itemToPatch.Name;
            itemFromStore.Description = itemToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{itemid}")]
        public ActionResult DeleteItem(
            int householdId, int itemId)
        {
            var household = _householdDataStore.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
                return NotFound();
            }

            var itemFromStore = household.Items
                .FirstOrDefault(i => i.Id == itemId);
            if (itemFromStore == null)
            {
                return NotFound();
            }

            household.Items.Remove(itemFromStore);

            _mailService.Send("Item deleted.",
                $"Item {itemFromStore.Name} with id {itemFromStore.Id} was deleted.");

            return NoContent();
        }
    }
}
