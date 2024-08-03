using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VendasWebMVC.Migrations;
using VendasWebMVC.Models;
using VendasWebMVC.Services;

namespace VendasWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salerRecordService;
        public SalesRecordsController(SalesRecordService salesRecordsService)
        {
            _salerRecordService = salesRecordsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? initial, DateTime? final)
        {
            try
            {
                if (!initial.HasValue)
                {
                    initial = new DateTime(DateTime.Now.Year, 1, 1);
                }
                if (!final.HasValue)
                {
                    final = DateTime.Now;
                }

                ViewData["minDate"] = initial.Value.ToString("yyyy-MM-dd");
                ViewData["maxDate"] = final.Value.ToString("yyyy-MM-dd");

                var salesRecords = await _salerRecordService.FindByDateAsync(initial, final);

                return View(salesRecords);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }

        }

        public async Task<IActionResult> GroupingSearch(DateTime? initial, DateTime? final)
        {
            try
            {
                if (!initial.HasValue)
                {
                    initial = new DateTime(DateTime.Now.Year, 1, 1);
                }
                if (!final.HasValue)
                {
                    final = DateTime.Now;
                }

                ViewData["minDate"] = initial.Value.ToString("yyyy-MM-dd");
                ViewData["maxDate"] = final.Value.ToString("yyyy-MM-dd");

                var salesRecords = await _salerRecordService.FindByDateGroupAsync(initial, final);

                return View(salesRecords);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }
    }
}

