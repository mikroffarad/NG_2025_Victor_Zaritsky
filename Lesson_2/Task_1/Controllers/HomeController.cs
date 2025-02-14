using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            string username = HttpContext.Session.GetString("Username");
            bool isAdmin = HttpContext.Session.GetInt32("IsAdmin") == 1;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Verify", "Login");
            }

            ViewBag.Username = username;
            ViewBag.Message = $"You {(isAdmin ? "are" : "are not")} admin";
            return View();
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
