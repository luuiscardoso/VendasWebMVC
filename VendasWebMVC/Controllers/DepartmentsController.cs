using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VendasWebMVC.Models;

namespace VendasWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<mdlDepartment> list = new List<mdlDepartment>();
            list.Add(new mdlDepartment { Id = 1, Name = "Eletronics" });
            list.Add(new mdlDepartment { Id = 2, Name = "Fashion" });
            return View(list);
        }
    }
}
