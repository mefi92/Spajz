using Microsoft.AspNetCore.Mvc;
using SpajzManager.Api.Models;

namespace SpajzManager.Api.Controllers
{
    [ApiController]
    [Route("api/households")]
    public class HouseholdsController : ControllerBase
    {
        private readonly HouseholdDataStore _householdDataStore;

        public HouseholdsController(HouseholdDataStore householdDataStore) 
        {
            _householdDataStore = householdDataStore ?? throw new AggregateException(nameof(householdDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<HouseholdDto>> GetHouseholds()
        {
            return Ok(_householdDataStore.Households);
        }

        [HttpGet("{id}")]
        public ActionResult<HouseholdDto> GetHousehold(int id)
        {
            var itemToReturn = _householdDataStore
                .Households.FirstOrDefault(h => h.Id == id);

            if (itemToReturn == null)
            {
                return NotFound();
            }

            return Ok(itemToReturn);        
        }
    }
}
