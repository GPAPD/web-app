using Microsoft.AspNetCore.Mvc;
using web_app.Data.Entity;
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

        public async Task<IActionResult> EditeItemDetails(long Id) 
        {
            DashbordModel model = new DashbordModel();
            if (Id > 0) 
            {
                model.Product = await _productsServies.GetProductById(Id);
                if (model.Product == null) 
                {
                    return RedirectToAction("Dashbord", "Admin");
                }
                
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveItem(Product content) 
        {
            if (content != null) 
            {
               
            }

            return RedirectToAction("Dashbord", "Admin");
        }
    }
}
