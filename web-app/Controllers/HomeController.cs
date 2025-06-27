using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_app.Data.IService;
using web_app.Models;

namespace web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServies _productServies;

        public HomeController(ILogger<HomeController> logger, IProductServies productServies)
        {
            _logger = logger;
            _productServies = productServies;
        }

        public async Task<IActionResult> Index()
        {
            HomeModel model = new HomeModel();

            model.ProductList = await _productServies.GetAllItems();


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
