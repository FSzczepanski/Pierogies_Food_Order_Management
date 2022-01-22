namespace CleanArchitecture.WebUI.Controllers
{
    using Application.Common.Interfaces;
    using Application.Common.Models;
    using Filters;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : ApiControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password incorrect" });

            return Ok(response);
        }

        
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    
    }
}