using System;

namespace VendasWebMVC.Services.Exceptions
{
    public class DbConcurrencyExcpeption : ApplicationException
    {
        public DbConcurrencyExcpeption(string message) : base(message) 
        {
        }
    }
}
