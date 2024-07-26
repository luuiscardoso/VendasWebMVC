using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VendasWebMVC.Models;
using VendasWebMVC.Services;

namespace VendasWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;   
        }
        public IActionResult Index()
        {
            List<Seller> sellers = _sellerService.FindAll();
            return View(sellers);
        }
    }
}
