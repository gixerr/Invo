using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Invoices.Api.Controllers
{
    [Route(BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get()
            => "Invoices API";
    }
}