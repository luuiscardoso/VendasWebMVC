using System;
using System.Linq;
using VendasWebMVC.Models;

namespace VendasWebMVC.Services.Extension
{
    public static class DateQueryExtension
    {
        public static IQueryable<SalesRecord> BiggerOrEqual(this IQueryable<SalesRecord> baseQuery, DateTime? initial)
        {
            if (initial.HasValue)
            {
                return baseQuery.Where(sale => sale.Date >= initial);
            }
            return baseQuery;
        }

        public static IQueryable<SalesRecord> SmallerOrEqual(this IQueryable<SalesRecord> baseQuery, DateTime? final)
        {
            if (final.HasValue)
            {
                return baseQuery.Where(sale => sale.Date <= final);
            }
            return baseQuery;
        }
    }
}
