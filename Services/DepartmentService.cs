using System.Collections.Generic;
using System.Linq;
using sales_web_mvc.Models;
using sales_web_mvc.Models.Data;

namespace sales_web_mvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebContext _context;

        public DepartmentService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Departments.OrderBy(x => x.Name).ToList();
        }
    }
}