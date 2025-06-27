using web_app.Data.Entity;

namespace web_app.Models
{
    public class HomeModel
    {
        public IEnumerable<Product> ProductList { get; set; }

    }
}
