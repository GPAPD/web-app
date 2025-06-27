using Microsoft.EntityFrameworkCore;
using web_app.Data.Entity;
using web_app.Models;

namespace web_app.Data.IService.ProductServies
{
    public class ProductServies : IProductServies
    {
        private readonly AppDbContext _db;
        private readonly ResponseDto _responseDto;

        public ProductServies(AppDbContext db, ResponseDto responseDto)
        {
            _db = db;
            _responseDto = responseDto;
        }

        public async Task<Product>? GetProductById(long Id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<ResponseDto> UpdateProduct(Product content)
        {
            try
            {
                if (content != null)
                {
                    _db.Update(content);
                    _responseDto.Result =  await _db.SaveChangesAsync();
                    _responseDto.IsSuccess = true;
                    _responseDto.Message = "Saved";
                }

                return _responseDto;
            }
            catch (Exception ex) 
            {
                _responseDto.Message = ex.Message;

                return _responseDto;
            }
            
        }

        async Task<IEnumerable<Product>>? IProductServies.GetAllItems(int perPage)
        {
            IEnumerable<Product> products = await _db.Products.Take(perPage).ToListAsync();

            return products;
        }


    }
}
