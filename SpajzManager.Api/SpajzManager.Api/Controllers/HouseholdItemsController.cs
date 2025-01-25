using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Models;

namespace SpajzManager.Api.Controllers
{
    [Route("api/households/{householdId}/householditems")]
    [ApiController]
    public class HouseholdItemsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<HouseholdItemDto>> GetHouseholdItems(int householdId)
        {
            var household = HouseholdDataStore.Current.Households
                .FirstOrDefault(h => h.Id == householdId);

            if (household == null)
            {
                return NotFound();
            }

            return Ok(household.Items);
        }

        [HttpGet("householditemid", Name = "GetHouseholdItem")]
        public ActionResult<HouseholdItemDto> GetHouseholdItem(
            int householdId, int householdItemId) 
        {
            var household = HouseholdDataStore.Current.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
                return NotFound();
            }

            var item = household.Items.FirstOrDefault(i => i.Id == householdItemId);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<HouseholdItemDto> CreateHouseholdItem(
            int householdId, 
            HouseholdItemForCreationDto householdItem)
        {
            var household = HouseholdDataStore.Current.Households
                .FirstOrDefault(h => h.Id == householdId);
            if (household == null)
            {
                return NotFound();
            }

            // temporary - to be improved
            var maxHouseholdItemId = HouseholdDataStore.Current.Households
                .SelectMany(h => h.Items).Max(i => i.Id);

            var finalHouseholdItem = new HouseholdItemDto()
            {
                Id = ++maxHouseholdItemId,
                Name = householdItem.Name,
                Description = householdItem.Description,
            };

            household.Items.Add(finalHouseholdItem);

            return CreatedAtRoute("GetHouseholdItem",
                new
                {
                    householdId,
                    householdItemId = finalHouseholdItem.Id
                },
                finalHouseholdItem);
        }
    }
}
