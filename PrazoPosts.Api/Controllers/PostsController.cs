using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrazoPosts.Model;

namespace PrazoPosts.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // GET posts
        [HttpGet]
        public ActionResult<IEnumerable<BlogPost>> Get()
        {
            return new BlogPost[] {
                new BlogPost { Title = "Blog 1"},
                new BlogPost { Title = "Blog 2"}
            };
        }

        // GET posts/{id}
        [HttpGet("{id}")]
        public ActionResult<BlogPost> Get(int id)
        {
            return new BlogPost { Title = "Blog 1" };
        }

        // POST posts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT posts/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE posts/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
