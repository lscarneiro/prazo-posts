using System;
using Microsoft.AspNetCore.Mvc;
using PrazoPosts.Dto;
using PrazoPosts.Service.Auth;
using PrazoPosts.Service.Exceptions;

namespace PrazoPosts.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        // POST auth
        [HttpPost]
        public IActionResult Post([FromBody] AuthDTO authData)
        {
            try
            {
                return Ok(_authService.Authenticate(authData));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.ToJson());
            }
        }
    }
}
