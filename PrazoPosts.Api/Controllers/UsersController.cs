using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrazoPosts.Dto;
using PrazoPosts.Service.Exceptions;
using PrazoPosts.Service.Users;

namespace PrazoPosts.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : PrazoController
    {
        IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET users
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetUser(CurrentUserId));
        }

        // POST users
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {

            _userService.RegisterUser(user);
            return Ok();
        }
    }
}
