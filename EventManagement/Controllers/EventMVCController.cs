using Microsoft.AspNetCore.Mvc;

namespace EventManagementAPI.Controllers
{
    public class EventMVCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
