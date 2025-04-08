using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SC_701_PAW_Lunes.Models;
using SC_701_PAW_Lunes.Resources;
using SC_701_PAW_Lunes.ViewModel;

namespace SC_701_PAW_Lunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<ShareResources> _recursos;

        public HomeController(IStringLocalizer<ShareResources> res)
        {
            _recursos = res;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
