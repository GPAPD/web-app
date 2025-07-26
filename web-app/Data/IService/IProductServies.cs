using web_app.Data.Entity;
using web_app.Models;

namespace web_app.Data.IService
{
    public interface IProductServies
    {
        public Task<IEnumerable<Product>>? GetAllItems();

        public Task<Product>? GetProductById(long Id);

        public Task<ResponseDto> UpdateProduct(Product content);

        public Task<ResponseDto> AddNewProduct(Product content);
    }
}
