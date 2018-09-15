using System;
using Microsoft.AspNetCore.Mvc;
using PrazoPosts.Dto;
using PrazoPosts.Service.Exceptions;
using PrazoPosts.Service.Interfaces;
using PrazoPosts.Service.Users;

namespace PrazoPosts.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // POST users
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO user)
        {
            try
            {
                _userService.RegisterUser(user);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.ToJson());
            }
        }
    }
}
