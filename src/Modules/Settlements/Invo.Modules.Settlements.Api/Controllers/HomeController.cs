using Microsoft.AspNetCore.Mvc;

namespace Invo.Modules.Settlements.Api.Controllers
{
    [Route(SettlementsModule.BasePath)]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get()
            => "Settlements API";
    }
}