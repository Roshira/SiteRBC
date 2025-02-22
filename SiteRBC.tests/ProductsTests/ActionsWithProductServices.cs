using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using System.Threading.Tasks;

namespace SiteRBC.Services
{
    public class ProductService
    {
        private readonly SiteRBCContext _context;

        public ProductService(SiteRBCContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductAsync(ReadyProduct product)
        {
            if (product == null)
                return false;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
