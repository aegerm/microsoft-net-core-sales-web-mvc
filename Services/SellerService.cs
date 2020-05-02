using System.Collections.Generic;
using System.Linq;
using sales_web_mvc.Models;
using sales_web_mvc.Models.Data;

namespace sales_web_mvc.Services
{
    public class SellerService
    {
        private readonly SalesWebContext _context;

        public SellerService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Sellers.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Sellers.Add(seller);
            _context.SaveChanges();
        }
    }
}