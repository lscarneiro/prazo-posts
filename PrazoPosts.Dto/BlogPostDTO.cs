using System;

namespace PrazoPosts.Dto
{
    public class BlogPostDTO
    {
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
        public string Content { get; set; }
        public string CoverUrl { get; set; }
    }
}
