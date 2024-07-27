using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VendasWebMVC.Models;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Services;

namespace VendasWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            List<Seller> sellers = _sellerService.FindAll();
            return View(sellers);
        }

        public IActionResult Create()
        {
            List<Department> departmets = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departmets };   
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller); 
            return RedirectToAction(nameof(Index));
        }
    }
}
