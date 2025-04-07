using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SC_701_PAW_Lunes.Models;
using SC_701_PAW_Lunes.ViewModel;

namespace SC_701_PAW_Lunes.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

  
    }
}
