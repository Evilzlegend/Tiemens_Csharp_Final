using Microsoft.AspNetCore.Mvc;
using DMVLaw.Data;

namespace DMVLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public AuthenticationController(JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }


        [HttpPost]
        public IActionResult AuthUser([FromBody] user user)
        {
            var token = jwtAuthenticationManager.Authentication(user.username, user.password, user.role);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }

    public class user
    {
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
