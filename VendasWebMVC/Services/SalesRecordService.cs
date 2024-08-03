using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly BdContext _bdContext;

        public SalesRecordService(BdContext bdContext)
        {
            _bdContext = bdContext;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initial, DateTime? final)
        {
            var result = from sale in _bdContext.SalesRecord select sale; // selecting all sales in SalesRecord table
            if (initial.HasValue) // if the var have a value, lets do a new consult
            {                                 //contain         
                result.Where(sale => sale.Date >= initial);
            }
            if (final.HasValue)
            {
                result.Where(sale => sale.Date <= final);
            }
            // in the final result,  we'll have a consult with the initial date e final date filterted
             return await result.Include(sale => sale.Seller)
                  .Include(sale => sale.Seller.Department)
                  .OrderByDescending(sale => sale.Date)
                  .ToListAsync(); // this command actually executes the query
        }

        public async Task< List<IGrouping<Department,SalesRecord> > > FindByDateGroupAsync(DateTime? initial, DateTime? final)
        {
            var result = from sale in _bdContext.SalesRecord select sale; 
            if (initial.HasValue) 
            {                                        
                result.Where(sale => sale.Date >= initial);
            }
            if (final.HasValue)
            {
                result.Where(sale => sale.Date <= final);
            }
            
            return await result.Include(sale => sale.Seller)
                 .Include(sale => sale.Seller.Department)
                 .OrderByDescending(sale => sale.Date)
                 .GroupBy(sale => sale.Seller.Department)
                 .ToListAsync(); 
        }
    }
}
