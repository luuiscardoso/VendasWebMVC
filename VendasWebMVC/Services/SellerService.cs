using System.Collections.Generic;
using System.Linq;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using VendasWebMVC.Services.Exceptions;
using System;

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
            Seller seller = _bdContext.Seller.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
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
            Seller seller = _bdContext.Seller.Find(id);
            if (seller == null)
            {
                throw new KeyNotFoundException($"Seller with Id {id} not found.");
            }
            _bdContext.Seller.Remove(seller);
            _bdContext.SaveChanges();
        }

        public void Update(Seller seller)
        {
            // if a seller don't exists in database we throw a excpetion
            if (!_bdContext.Seller.Any(s => s.Id == seller.Id))
            {
                throw new NotFoundException("Seller not found");
            }

            try
            {
                _bdContext.Update(seller);
                _bdContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyExcpeption(e.Message);
            }
            
        }
    }
}
