using System;
using Microsoft.AspNetCore.Mvc;

namespace PrazoPosts.Api.Controllers
{
    public class PrazoController : ControllerBase
    {
        protected string CurrentUserId => HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    }
}
