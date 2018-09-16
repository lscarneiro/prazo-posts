using System;
using System.IdentityModel.Tokens.Jwt;
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
    public class AuthorsController : PrazoController
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
            var authors = _authorService.GetAuthors(CurrentUserId);
            return Ok(authors);
        }

        // POST authors
        [HttpPost]
        public IActionResult Post([FromBody] AuthorDTO author)
        {
            _authorService.CreateAuthor(CurrentUserId, author);
            return Ok();
        }

        // PUT authors/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AuthorDTO author)
        {
            _authorService.UpdateAuthor(CurrentUserId, id, author);
            return Ok();
        }

        // DELETE authors/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _authorService.DeleteAuthor(CurrentUserId, id);
            return Ok();
        }
    }

}
