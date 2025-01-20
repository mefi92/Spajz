using Microsoft.AspNetCore.Mvc;

namespace SpajzManager.Api.Controllers
{
    [ApiController]
    [Route("api/householditems")]
    public class HouseholdItemsController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetHouseholdItems()
        {
            return new JsonResult(
                new List<object>
                {
                    new { id = 1, Name = "Alma"},
                    new { id = 2, Name = "Tej"}
                });
        }
    }
}
