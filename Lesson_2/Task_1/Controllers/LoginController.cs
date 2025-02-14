using Microsoft.AspNetCore.Mvc;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class LoginController : Controller
    {
        private List<UserModel> Users = new()
        {
            new UserModel {Id = 1, Username="admin", Password="admin123", IsAdmin=true},
            new UserModel {Id = 2, Username="user", Password="user123", IsAdmin=false},
        };

        public IActionResult Verify() => View();

        [HttpPost]
        public IActionResult Verify(string username, string password)
        {
            var user = Users.FirstOrDefault(wantedUser => wantedUser.Username == username && wantedUser.Password == password);

            if (user == null)
            {
                ViewBag.Message = "Invalid user or password";
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetInt32("IsAdmin", user.IsAdmin ? 1 : 0);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Verify");
        }
    }
}
