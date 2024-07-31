using System.Collections.Generic;
using System.Linq;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;
using VendasWebMVC.Services.Exceptions;
using System;
using System.Threading.Tasks;

namespace VendasWebMVC.Services
{
    public class SellerService
    {
        private readonly BdContext _bdContext;

        public SellerService(BdContext bdContext)
        {
            _bdContext = bdContext;
        }

        public async Task<List<Seller>> FindAllAsync() // ver todos
        {
            return await _bdContext.Seller.ToListAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            Task<Seller> seller = _bdContext.Seller.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id);
            if (seller == null)
            {
                throw new NotFoundException($"Seller with Id {id} not found.");
            }
            return await seller;
        }

        public async Task InsertAsync(Seller seller)
        {
            _bdContext.Add(seller);
            await _bdContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await _bdContext.Seller.FindAsync(id);
            _bdContext.Seller.Remove(seller);
            await _bdContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _bdContext.Seller.AnyAsync(s => s.Id == seller.Id);
            // if a seller don't exists in database we throw a excpetion
            if (!hasAny)
            {
                throw new NotFoundException("Seller not found");
            }

            try
            {
                _bdContext.Update(seller);
                await _bdContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyExcpeption(e.Message);
            }
            
        }
    }
}
