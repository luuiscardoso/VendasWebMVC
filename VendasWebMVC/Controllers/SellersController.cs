using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            List<Seller> sellers = await _sellerService.FindAllAsync();
            return View(sellers);
        }

        //GET
        public async Task<IActionResult> Create()
        {
            List<Department> departmets =  await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departmets };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        //GET 
        public async Task<IActionResult> DeleteConfirmation(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                Seller seller = await _sellerService.FindByIdAsync(id.Value);

                return View(seller);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                Seller seller = await _sellerService.FindByIdAsync(id.Value);

                return View(seller);
            }
            catch (KeyNotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
        }

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

                List<Department> departments = await _departmentService.FindAllAsync();
                Seller seller = await _sellerService.FindByIdAsync(id.Value);

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
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
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
