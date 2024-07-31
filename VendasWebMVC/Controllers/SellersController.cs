using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VendasWebMVC.Models;
using VendasWebMVC.Models.ViewModels;
using VendasWebMVC.Services;
using VendasWebMVC.Services.Exceptions;

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

        //GET
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

        //GET 
        public IActionResult DeleteConfirmation(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                Seller seller = _sellerService.FindById(id.Value);

                return View(seller);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                Seller seller = _sellerService.FindById(id.Value);

                return View(seller);
            }
            catch (KeyNotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
        }

        // GET
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                List<Department> departments = _departmentService.FindAll();
                Seller seller = _sellerService.FindById(id.Value);

                SellerFormViewModel sf = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(sf);
            }
            catch (KeyNotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }
    }
}
