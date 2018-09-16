using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrazoPosts.Dto;
using PrazoPosts.Service.Authors;
using PrazoPosts.Service.Exceptions;

namespace PrazoPosts.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        }

        // GET authors
        [HttpGet]
        public IActionResult Get()
        {
            var name = User.Identity.Name;
            var authors = _authorService.GetAuthors();
            return Ok(authors);
        }

        // POST authors
        [HttpPost]
        public IActionResult Post([FromBody] AuthorDTO author)
        {
            try
            {
                _authorService.CreateAuthor(author);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.ToJson());
            }
        }
    }

}
