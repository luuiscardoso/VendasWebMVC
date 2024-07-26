using Microsoft.AspNetCore.Mvc;

namespace VendasWebMVC.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
