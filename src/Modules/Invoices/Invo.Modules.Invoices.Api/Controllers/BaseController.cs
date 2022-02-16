using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Invoices.Api.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    internal class BaseController : ControllerBase
    {
        protected const string BasePath = "invoices-module";

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