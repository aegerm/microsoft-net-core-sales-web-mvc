using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Sellers.Add(seller);

            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FinByIdAsync(int id)
        {
            return await _context.Sellers.Include(obj => obj.Department).FirstOrDefaultAsync(x => x.Id == id); //EagerLoading
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var value = await _context.Sellers.FindAsync(id);

                _context.Sellers.Remove(value);

                await _context.SaveChangesAsync();
            }
            
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Sellers.AnyAsync(x => x.Id == seller.Id);

            if (! hasAny)
            {
                throw new NotFoundException("Id not found!");
            }

            try
            {
                _context.Update(seller);

                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}