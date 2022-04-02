using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Settlements.Api.Controllers
{
    [ApiController]
    [Route(SettlementsModule.BasePath + "/[controller]")]
    public class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}