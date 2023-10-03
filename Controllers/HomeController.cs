using DotnetCoreVCB.Common.Filter;
using DotnetCoreVCB.Models;
using DotnetCoreVCB.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;

namespace DotnetCoreVCB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository userRepository;
        private IDistributedCache _cache;
        public HomeController(ILogger<HomeController> logger, IUserRepository userRepo, IDistributedCache cache)
        {
            _logger = logger;
            this.userRepository = userRepo;
            this._cache = cache;
        }

        [ServiceFilter(typeof(SampleActionFilter))]
        public IActionResult Index()
        {
            var lstUsers = this.userRepository.GetUsers();

            //Save to distributed cache
            String list2json = Newtonsoft.Json.JsonConvert.SerializeObject(lstUsers);
            byte[] save2Cache = System.Text.Encoding.UTF8.GetBytes(list2json);

            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));

            this._cache.Set("ListUser", save2Cache, options);
            return View();
        }

        [HttpGet("/my-privacy/")]
        public IActionResult Privacy()
        {
            //Get from distributed cache
            byte[] valueFromCache = this._cache.Get("ListUser");
            String text2Json = System.Text.Encoding.UTF8.GetString(valueFromCache);
            List<SimpleUser> lstUser = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUser>>(text2Json);

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