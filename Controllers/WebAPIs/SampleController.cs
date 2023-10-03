using DotnetCoreVCB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreVCB.Controllers.WebAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<SimpleUser> users = new List<SimpleUser>();
            users.Add(new SimpleUser()
            {
                UserName = "Khanh.tx@euroland.com",
                Password = "password",
                Roles = "Admin"
            });
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Post(SimpleUser user)
        {
            return Ok(user);
        }
    }
}
