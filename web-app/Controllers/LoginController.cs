using Microsoft.AspNetCore.Mvc;

namespace web_app.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
