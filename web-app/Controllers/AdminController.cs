using Microsoft.AspNetCore.Mvc;
using web_app.Data.Entity;
using web_app.Data.IService;
using web_app.Models;
using System.IO;
using web_app.Service.IService;

namespace web_app.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductServies _productsServies;
        private readonly ISearchService _searchService ;
        public AdminController(IProductServies productServies, ISearchService searchService) 
        {
            _productsServies = productServies;
            _searchService = searchService;
        }
        public async Task<IActionResult> Dashbord()
        {
            DashbordModel model = new DashbordModel();
            model.ProductList = await _productsServies.GetAllItems();

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
            var predictable = new List<string> { "Amino", "Acid", "Fat Burner", "Herbal", "Hydration", "Mineral", "Omega", "Performance", "Protein", "Sleep Aid", "Vitamin" };

            //if (content != null && content.Category != null && predictable.Contains(content.Category) && content.Price > 0 && content.Price < 100)
            //{
                ResponseDto responseDto = await _productsServies.UpdateProduct(content);
                return RedirectToAction("Dashbord", "Admin");
            //}
            //else 
            //{
            //    DashbordModel model = new DashbordModel();
            //    model.Product = content;
            //    ModelState.AddModelError("CustomError","This items is over priced");
            //    return View("EditeItemDetails", model);
            //}           
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

        public async Task<IEnumerable<Product>> GetAllProducts() 
        {
            IEnumerable<Product> productlist = new List<Product>();    


            return productlist;
        }

        [HttpPost]
        public async Task<bool> ExportDataIntoCSV()
        {
            IEnumerable<Product> productsList = new List<Product>();

            try
            {
                productsList = await _productsServies.GetAllItems();

                if (productsList == null || !productsList.Any())
                    return false;

                //string folderPath = @"C:\Users\akash\Desktop\doc-reder\pravixMatic\ai-assistant\backend\item_data";
                //server root
                string folderPath = @"C:\SearchApi\app\backend\item_data";
                string fileName = "testdata.csv";
                string fullPath = Path.Combine(folderPath, fileName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                using (StreamWriter writer = new StreamWriter(fullPath, false)) // false = overwrite
                {
                    writer.AutoFlush = true;

                    // header
                    writer.WriteLine("item_id,item_description,item_price,item_category");

                    // rows
                    foreach (var product in productsList)
                    {
                        string id = product.Id.ToString();
                        string description = EscapeForCsv(product.ProductDesc);
                        string price = product.Price.ToString();
                        string category = EscapeForCsv(product.Category);

                        writer.WriteLine($"{id},{description},{price},{category}");
                    }
                }

                //send api request 
                var updated = false;

                ResponseDto response = await _searchService.UpdateIndexing();
                if (response != null) 
                {
                    updated = response.IsSuccess;

                    return updated;
                }
                return updated;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        // Helper method to handle commas and quotes in CSV
        private string EscapeForCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                value = value.Replace("\"", "\"\""); // Escape quotes
                return $"\"{value}\""; // Wrap in quotes
            }
            return value;
        }

    }
}
