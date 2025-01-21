using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Models;

namespace SpajzManager.Api.Controllers
{
    [ApiController]
    [Route("api/households")]
    public class HouseholdsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<HouseholdDto>> GetHouseholds()
        {
            return Ok(HouseholdDataStore.Current.Households);
        }

        [HttpGet("{id}")]
        public ActionResult<HouseholdDto> GetHousehold(int id)
        {
            var itemToReturn = HouseholdDataStore.Current
                .Households.FirstOrDefault(h => h.Id == id);

            if (itemToReturn == null)
            {
                return NotFound();
            }

            return Ok(itemToReturn);        
        }
    }
}
