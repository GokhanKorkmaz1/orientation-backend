using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrientationAPI.Business;
using OrientationAPI.Helpers;
using OrientationAPI.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrientationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        //private IConfiguration _configuration;
        public static string userRole = Constants.guestRole;
        public static int userId = 0;

        public LoginController(IUserService userService/*, IConfiguration configuration*/)
        {
            _userService = userService;
            //_configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login([FromBody]LoginUserDto loginUserDto)
        {
            //if (userRole != Constants.guestRole)
            //{
            //    return BadRequest("You must log out of your account.");
            //}
            var user = _userService.GetByEmail(loginUserDto.email);
            if (user == null)
            {
                return Unauthorized("Check your e-mail address.");
            }

            userRole = user.isAdmin == true ? Constants.adminRole : Constants.userRole;
            userId = user.id;
            if (user.password != loginUserDto.password)
            {
                return NotFound("Check your password.");
            }
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            //        new Claim(ClaimTypes.Name, user.name),
            //        new Claim(ClaimTypes.Role, userRole)
            //    }),
            //    Expires = DateTime.Now.AddDays(1),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);

            return Ok(user);
        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            if(userRole!= Constants.guestRole)
            {
                userRole = Constants.guestRole;
                userId = 0;
                return Ok();
            }
            return BadRequest();
        }



    }
}
