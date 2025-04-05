using Microsoft.AspNetCore.Mvc;

namespace SC_701_PAW_Lunes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
