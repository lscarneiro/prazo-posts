using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrazoPosts.Dto;
using PrazoPosts.Service.Exceptions;
using PrazoPosts.Service.Users;

namespace PrazoPosts.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : PrazoController
    {
        IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET users
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetUser(CurrentUserId));
        }

        // POST users
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {
            return Ok(_userService.RegisterUser(user));
        }
    }
}
