using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VendasWebMVC.Migrations;
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
            if (initial.HasValue)
            {
                ViewData["minDate"] = initial.Value.ToString("yyyy-MM-dd");
            }
            if (final.HasValue)
            {
                ViewData["maxDate"] = final.Value.ToString("yyyy-MM-dd");
            }
           
            var salesRecords = await _salerRecordService.FindByDateAsync(initial, final);
            
            return View(salesRecords);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? initial, DateTime? final)
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
    }
}

