using Microsoft.AspNetCore.Mvc;
using web_app.Models;

namespace web_app.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            LogInRequest model = new LogInRequest();

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LogInRequest model)
        {
            //LogInRequest model = new LogInRequest();
            if (model.Email != null && model.Password != null) 
            {
                if (model.Email == "akash" && model.Password == "admin123") 
                {
                    return RedirectToAction("Dashbord", "Admin");
                }
                ModelState.AddModelError("CustomError", "Incorrect username or password");
            }
            return View(model);
        }
    }
}
