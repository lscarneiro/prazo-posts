using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrazoPosts.Dto;
using PrazoPosts.Model;
using PrazoPosts.Service.BlogPosts;

namespace PrazoPosts.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class PostsController : PrazoController
    {
        IBlogPostService _blogPostService;
        public PostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService ?? throw new ArgumentNullException(nameof(blogPostService));
        }

        // GET posts
        [HttpGet]
        public IActionResult Get()
        {
            var authors = _blogPostService.GetBlogPosts(CurrentUserId);
            return Ok(authors);
        }

        // POST posts
        [HttpPost]
        public IActionResult Post([FromBody] BlogPostDTO post)
        {
            _blogPostService.CreateBlogPost(CurrentUserId, post);
            return Ok();
        }

        // PUT posts/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] BlogPostDTO post)
        {
            _blogPostService.UpdateBlogPost(CurrentUserId, id, post);
            return Ok();
        }

        // DELETE posts/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _blogPostService.DeleteBlogPost(CurrentUserId, id); 
            return Ok();
        }
    }
}
