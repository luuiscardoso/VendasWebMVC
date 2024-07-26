using System.Collections.Generic;
using System.Linq;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Services
{
    public class SellerService
    {
        private readonly BdContext _bdContext;

        public SellerService(BdContext bdContext)
        {
            _bdContext = bdContext;
        }

        public List<Seller> FindAll() // ver todos
        {
            return _bdContext.Seller.ToList();
        }
    }
}
