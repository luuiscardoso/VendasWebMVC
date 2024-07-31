using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMVC.Data;
using VendasWebMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace VendasWebMVC.Services
{
    public class DepartmentService
    {
        private readonly BdContext _bdContext;

        public DepartmentService(BdContext bdContext)
        {
            _bdContext = bdContext;
        }

        public async Task<List<Department>> FindAllAsync() // ver todos
        {
            return await _bdContext.Department.OrderBy(d => d.Name).ToListAsync();
        }
    }
}
