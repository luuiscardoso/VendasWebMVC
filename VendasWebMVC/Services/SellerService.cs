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

        public Seller FindById(int id)
        {
            var seller = _bdContext.Seller.FirstOrDefault(s => s.Id == id);
            if (seller == null)
            {
                throw new KeyNotFoundException($"Seller with Id {id} not found.");
            }
            return seller;
        }

        public void Insert(Seller seller)
        {
            _bdContext.Add(seller);
            _bdContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var seller = _bdContext.Seller.Find(id);
            if (seller == null)
            {
                throw new KeyNotFoundException($"Seller with Id {id} not found.");
            }
            _bdContext.Seller.Remove(seller);
            _bdContext.SaveChanges();
        }

    }
}
