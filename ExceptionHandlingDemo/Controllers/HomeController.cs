using System.Diagnostics;
using ExceptionHandlingDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlingDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id = null)
        {
            if (id.HasValue)
            {
                if (id == 1)
                {
                    throw new FileNotFoundException("File not found exception thrown in index.chtml");
                }
                else if (id == 2)
                {
                    return StatusCode(500);
                }
            }
            return View();
        }

        public IActionResult MyStatusCode(int code)
        {
            if (code == 404)
            {
                ViewBag.ErrorMessage = "The requested page not found.";
            }
            else if (code == 500)
            {
                ViewBag.ErrorMessage = "My custom 500 error message.";
            }
            else
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
            }

            ViewBag.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ViewBag.ShowRequestId = !string.IsNullOrEmpty(ViewBag.RequestId);
            ViewBag.ErrorStatusCode = code;

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
