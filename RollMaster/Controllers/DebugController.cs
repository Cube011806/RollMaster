using Microsoft.AspNetCore.Mvc;

namespace RollMaster.Controllers
{
    public class DebugController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
