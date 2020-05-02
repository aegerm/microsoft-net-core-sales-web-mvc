using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using sales_web_mvc.Models;
using sales_web_mvc.Models.Data;
using sales_web_mvc.Services.Exceptions;

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
            return _context.Sellers.Include(obj => obj.Department).FirstOrDefault(x => x.Id == id); //EagerLoading
        }

        public void Remove(int id)
        {
            var value = _context.Sellers.Find(id);

            _context.Sellers.Remove(value);

            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if (!_context.Sellers.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Id not found!");
            }

            try
            {
                _context.Update(seller);

                _context.SaveChanges();
            }

            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}