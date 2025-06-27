using Microsoft.EntityFrameworkCore;
using web_app.Data.Entity;

namespace web_app.Data.IService.ProductServies
{
    public class ProductServies : IProductServies
    {
        private readonly AppDbContext _db;

        public ProductServies(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product>? GetProductById(long Id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == Id);
        }

        async Task<IEnumerable<Product>>? IProductServies.GetAllItems(int perPage)
        {
            IEnumerable<Product> products = await _db.Products.Take(perPage).ToListAsync();

            return products;
        }
    }
}
