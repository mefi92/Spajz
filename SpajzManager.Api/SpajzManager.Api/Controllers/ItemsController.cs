using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Models;

namespace SpajzManager.Api.Controllers
{
    [Route("api/households/{householdId}/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetItems(int householdId)
        {
            var household = HouseholdDataStore.Current.Households
                .FirstOrDefault(h => h.Id == householdId);

            if (household == null)
            {
                return NotFound();
            }

            return Ok(household.Items);
        }

        [HttpGet("{itemid}", Name = "GetItem")]
        public ActionResult<ItemDto> GetItem(
            int householdId, int itemId) 
        {
            var household = HouseholdDataStore.Current.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
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
            var household = HouseholdDataStore.Current.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
                return NotFound();
            }

            // temporary - to be improved
            var maxItemId = HouseholdDataStore.Current.Households
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
            var household = HouseholdDataStore.Current.Households
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
            var household = HouseholdDataStore.Current.Households
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
            var household = HouseholdDataStore.Current.Households
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

            return NoContent();
        }
    }
}
