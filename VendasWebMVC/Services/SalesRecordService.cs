using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using VendasWebMVC.Services.Exceptions;
using VendasWebMVC.Services.Extension;

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
            if(initial.Value > final.Value)
            {
                throw new DomainException("The start date must be less than the end date.");
            }

            var result = _bdContext.SalesRecord.AsQueryable() // create a base query by converting IEnumerable into IQueryable
                                               .BiggerOrEqual(initial) // sending base query to be manipulated
                                               .SmallerOrEqual(final)
                                               .Include(sale => sale.Seller)
                                               .Include(sale => sale.Seller.Department)
                                               .OrderByDescending(sale => sale.Date)
                                               .ToListAsync();

            return await result;
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupAsync(DateTime? initial, DateTime? final)
        {
            if (initial.Value > final.Value)
            {
                throw new DomainException("The start date must be less than the end date.");
            }

            var query = _bdContext.SalesRecord.AsQueryable()
                                              .BiggerOrEqual(initial.Value)
                                              .SmallerOrEqual(final.Value)
                                              .Include(sale => sale.Seller)
                                              .Include(sale => sale.Seller.Department)
                                              .OrderByDescending(sale => sale.Date)
                                              .GroupBy(sale => sale.Seller.Department)
                                              .ToListAsync();

            return await query;
        }
    }
}
