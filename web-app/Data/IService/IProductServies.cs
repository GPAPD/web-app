using web_app.Data.Entity;

namespace web_app.Data.IService
{
    public interface IProductServies
    {
        public Task<IEnumerable<Product>>? GetAllItems(int perPage = 10);
        public Task<Product>? GetProductById(long Id);
    }
}
