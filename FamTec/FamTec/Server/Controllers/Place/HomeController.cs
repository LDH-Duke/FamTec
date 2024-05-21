using Microsoft.AspNetCore.Mvc;

namespace FamTec.Server.Controllers.Place
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
