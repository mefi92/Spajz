using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Models;

namespace SpajzManager.Api.Controllers
{
    [ApiController]
    [Route("api/householditems")]
    public class HouseholdItemsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<HouseholdItemDto>> GetHouseholdItems()
        {
            return Ok(HouseholdItemDataStore.Current.HouseholdItems);
        }

        [HttpGet("{id}")]
        public ActionResult<HouseholdItemDto> GetHouseholdItem(int id)
        {
            var itemToReturn = HouseholdItemDataStore.Current
                .HouseholdItems.FirstOrDefault(i => i.Id == id);

            if (itemToReturn == null)
            {
                return NotFound();
            }

            return Ok(itemToReturn);            
        }
    }
}
