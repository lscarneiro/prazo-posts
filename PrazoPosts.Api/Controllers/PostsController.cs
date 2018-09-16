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
    public class PostsController : ControllerBase
    {
        IBlogPostService _blogPostService;
        public PostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService ?? throw new ArgumentNullException(nameof(blogPostService));
        }

        // GET posts
        [HttpGet]
        public ActionResult<IEnumerable<BlogPostDTO>> Get()
        {
            return new BlogPostDTO[] {
                new BlogPostDTO { Title = "Blog 1"},
                new BlogPostDTO { Title = "Blog 2"}
            };
        }

        // GET posts/{id}
        [HttpGet("{id}")]
        public ActionResult<BlogPostDTO> Get(int id)
        {
            return new BlogPostDTO { Title = "Blog 1" };
        }

        // POST posts
        [HttpPost]
        public void Post([FromBody] BlogPostDTO value)
        {
        }

        // PUT posts/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BlogPostDTO value)
        {
        }

        // DELETE posts/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
