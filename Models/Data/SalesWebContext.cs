using Microsoft.EntityFrameworkCore;

namespace sales_web_mvc.Models.Data
{
    public class SalesWebContext : DbContext
    {
        public SalesWebContext(DbContextOptions<SalesWebContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
    }
}