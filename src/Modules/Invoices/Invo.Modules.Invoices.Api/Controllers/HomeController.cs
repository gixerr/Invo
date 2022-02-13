using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Invoices.Api.Controllers
{
    [Route("invoices-module")]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
            => "Invoices API";
    }
}