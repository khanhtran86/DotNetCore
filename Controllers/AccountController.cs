using DotnetCoreVCB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotnetCoreVCB.Controllers
{

    public class AccountController : Controller
    {
        private IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IResult Token(SimpleUser user)
        {
            if (user.UserName == "khanhtran" && user.Password == "khanhtran123")
            {
                var issuer = this._configuration["Jwt:Issuer"];
                var audience = this._configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(this._configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString()
                    )
                }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Results.Ok(new { token = stringToken });
            }
            return Results.Unauthorized();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetLoggedUser()
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            return Ok(user.Value);
        }
    }
}
