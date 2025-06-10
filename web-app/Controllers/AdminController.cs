using Microsoft.AspNetCore.Mvc;
using web_app.Models;

namespace web_app.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashbord()
        {
            LogInRequest model = new LogInRequest();

            return View(model);
        }
    }
}
