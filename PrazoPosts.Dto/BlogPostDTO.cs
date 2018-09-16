using System;

namespace PrazoPosts.Dto
{
    public class BlogPostDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
        public string Content { get; set; }
    }
}
