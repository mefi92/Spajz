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

        [HttpGet("householditemid")]
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
    }
}
