using Microsoft.AspNetCore.Mvc;

namespace VendasWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
