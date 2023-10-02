using DotnetCoreVCB.Common.Filter;
using DotnetCoreVCB.Models;
using DotnetCoreVCB.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotnetCoreVCB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository userRepository;
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepo)
        {
            _logger = logger;
            this.userRepository = userRepo;
        }

        [ServiceFilter(typeof(SampleActionFilter))]
        public IActionResult Index()
        {
            var lstUsers = this.userRepository.GetUsers();
            return View();
        }

        [HttpGet("/my-privacy/")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult PostData()
        {
            return View();
        }
    }
}