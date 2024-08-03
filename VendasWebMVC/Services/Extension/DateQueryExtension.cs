using System;
using System.Linq;
using VendasWebMVC.Models;

namespace VendasWebMVC.Services.Extension
{
    public static class DateQueryExtension
    {
        public static IQueryable<SalesRecord> BiggerOrEqual(this IQueryable<SalesRecord> baseQuery, DateTime? initial)
        {
            return baseQuery.Where(sale => sale.Date >= initial);
        }

        public static IQueryable<SalesRecord> SmallerOrEqual(this IQueryable<SalesRecord> baseQuery, DateTime? final)
        {
            return baseQuery.Where(sale => sale.Date <= final);
        }
    }
}
