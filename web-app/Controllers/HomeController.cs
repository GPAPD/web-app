using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using web_app.Data.Entity;
using web_app.Data.IService;
using web_app.Models;
using web_app.Service.IService;

namespace web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServies _productServies;
        private readonly ISearchService _searchService;

        public HomeController(ILogger<HomeController> logger, IProductServies productServies, ISearchService searchService)
        {
            _logger = logger;
            _productServies = productServies;
            _searchService = searchService;
        }

        public async Task<IActionResult> Index()
        {
            HomeModel model = new HomeModel();

            model.ProductList = await _productServies.GetAllItems();


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchApi(string search)
        {
            HomeModel model = new HomeModel();
            var obj = new { message = search };
            var result = await _searchService.GetSearchData(obj);

            if (result != null && result.IsSuccess == true) 
            {
                var data = result.Result as SearchResponse;
                if (data != null && data.Results != null && data.Results.Count >0) 
                {
                    var arr = new List<long>();
                    foreach (var item in data.Results) 
                    {
                        //resulut should score should be greter that 0.3
                        if (item.Score >= 0.35) 
                        {
                            // Pattern to extract item_id
                            var match = Regex.Match(item.Content, @"item_id:\s*(\d+)");
                            if (match.Success)
                            {
                                string itemId = match.Groups[1].Value;
                                arr.Add(long.Parse(itemId));
                                //Console.WriteLine("Item ID: " + itemId);  // Output: 1001
                            }
                        }

                    }

                    List<Product> SearchProductList = new List<Product>();

                    foreach (var item in arr) 
                    {
                        var products = await _productServies.GetProductById(item);
                        if (products !=null) 
                        {
                            SearchProductList.Add(products);
                        }

                    }
                    model.ProductList = SearchProductList;
                }
            }
            model.SearchQuary = search;    
            
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
