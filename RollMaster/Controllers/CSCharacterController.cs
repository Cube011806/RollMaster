using Microsoft.AspNetCore.Mvc;

namespace RollMaster.Controllers
{
    public class CSCharacterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
