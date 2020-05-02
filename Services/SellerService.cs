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

        public Seller FinById(int id)
        {
            return _context.Sellers.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var value = _context.Sellers.Find(id);

            _context.Sellers.Remove(value);

            _context.SaveChanges();
        }
    }
}