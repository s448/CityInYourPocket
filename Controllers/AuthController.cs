using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInYourPocket.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        public IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUser user;
        public AuthController(IConfiguration configuration, ApplicationDbContext dbContext, IUser user)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            this.user = user;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]User _user)
        {
            if (_user != null && _user.Phone != null && _user.Password != null)
            {
                return await Authenticate(_user);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] User _user)
        {
            User? res = await _dbContext.users.FirstOrDefaultAsync(x => x.Phone == _user.Phone);
            if (res ==null)
            {
                user.AddUser(_user);
                return await Authenticate(_user);
            }
            else
            {
                return BadRequest(res.Phone);
            }
        }

        private async Task<IActionResult> Authenticate(User _user)
        {
            var user = await GetUser(_user.Phone, _user.Password);

            if (user != null)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("username", user.Username),
                        new Claim("Phone", user.Phone)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMonths(2),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }

        private async Task<User> GetUser(string phone, string password)
        {
            return await _dbContext.users.FirstOrDefaultAsync(u => u.Phone == phone && u.Password == password);
        }
    }
}
