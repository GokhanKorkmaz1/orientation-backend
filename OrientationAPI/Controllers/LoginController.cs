using Microsoft.AspNetCore.Mvc;
using OrientationAPI.Business;
using OrientationAPI.Helpers;
using OrientationAPI.Models.Dtos;

namespace OrientationAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        public static string userRole = Constants.guestRole;
        public static int userId = 0;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult Login([FromBody]LoginUserDto loginUserDto)
        {
            if (userRole != Constants.guestRole)
            {
                return BadRequest("You must log out of your account.");
            }

            var user = _userService.GetByEmail(loginUserDto.email);
            if (user == null)
            {
                return Unauthorized("Check your e-mail and password.");
            }

            userRole = user.isAdmin == true ? Constants.adminRole : Constants.userRole;
            userId = user.id;
            if (user.password != loginUserDto.password)
            {
                return NotFound("Check your e-mail and password");
            }

            return Ok(user);
        }

        [HttpGet("logout")]
        public ActionResult Logout()
        {
            if(userRole!= Constants.guestRole)
            {
                userRole = Constants.guestRole;
                userId = 0;

                AuthUser authUser = new AuthUser
                {
                    UserId = userId,
                    UserRole = userRole
                };

                return Ok(authUser);
            }
            return BadRequest();
        }



    }
}
