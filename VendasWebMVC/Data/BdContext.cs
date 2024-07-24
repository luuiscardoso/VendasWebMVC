using Microsoft.EntityFrameworkCore;

namespace VendasWebMVC.Data
{
    public class BdContext : DbContext
    {
        public BdContext(DbContextOptions <BdContext> options) : base(options)
        {
        }

        public DbSet<VendasWebMVC.Models.mdlDepartment> Department { get; set; }
    }
}

