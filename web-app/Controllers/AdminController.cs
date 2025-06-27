using Microsoft.AspNetCore.Mvc;
using web_app.Data.IService;
using web_app.Models;

namespace web_app.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductServies _productsServies;
        public AdminController(IProductServies productServies) 
        {
            _productsServies = productServies;
        }
        public async Task<IActionResult> Dashbord()
        {
            DashbordModel model = new DashbordModel();
            model.ProductList = await _productsServies.GetAllItems(10);

            return View(model);
        }

        public IActionResult EditeItemDetails() 
        {

            return View();
        }
    }
}
