using Microsoft.AspNetCore.Mvc;

namespace RollMaster.Controllers
{
    public class CBCharacterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
