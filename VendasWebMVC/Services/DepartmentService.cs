using System.Collections.Generic;
using System.Linq;
using VendasWebMVC.Data;
using VendasWebMVC.Models;

namespace VendasWebMVC.Services
{
    public class DepartmentService
    {
        private readonly BdContext _bdContext;

        public DepartmentService(BdContext bdContext)
        {
            _bdContext = bdContext;
        }

        public List<Department> FindAll() // ver todos
        {
            return _bdContext.Department.OrderBy(d => d.Name).ToList();
        }
    }
}
