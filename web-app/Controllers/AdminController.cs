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
            model.ProductList = await _productsServies.GetAllItems(100);

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
        public async Task<IActionResult> UpdateItem(Product content) 
        {
            if (content != null) 
            {
                ResponseDto responseDto = await _productsServies.UpdateProduct(content);
            }
            return RedirectToAction("Dashbord", "Admin");
        }

        public IActionResult AddNewItem() 
        {
            DashbordModel model = new DashbordModel();
            var cat = new List<string> { "Electronics", "Clothing", "Books", "Home", "Office", "Amino", "Acid", "Fat Burner", "Herbal", "Hydration", "Mineral", "Omega", "Performance", "Protein", "Sleep Aid", "Vitamin" };

            model.ItemCatogories = cat;

            return View(model);
        }

        public async Task<IActionResult> SaveNewItem(Product content) 
        {
            if (content != null) 
            {
                ResponseDto responseDto = await _productsServies.AddNewProduct(content);
            }
            return RedirectToAction("Dashbord", "Admin");
        }
    }
}
