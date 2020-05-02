using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using sales_web_mvc.Models;
using sales_web_mvc.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace sales_web_mvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;

        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initial, DateTime? final)
        {
            var result = from obj in _context.SalesRecords select obj;

            if (initial.HasValue)
            {
                result = result.Where(x => x.Date >= initial.Value);
            }

            if (final.HasValue)
            {
                result = result.Where(x => x.Date <= final.Value);
            }

            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}