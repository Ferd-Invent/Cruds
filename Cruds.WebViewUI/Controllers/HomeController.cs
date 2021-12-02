using Cruds.Infra;
using Cruds.WebViewUI.Models;
using LinqToDB.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cruds.WebViewUI.Controllers
{
    public class HomeController : Controller
    {
        public RepoLinqTodb repoLinqTodb;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index() {

           
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