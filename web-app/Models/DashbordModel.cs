using web_app.Data.Entity;

namespace web_app.Models
{
    public class DashbordModel
    {
        public IEnumerable<Product> ProductList { get; set; }

        public Product Product { get; set; }
    }
}
