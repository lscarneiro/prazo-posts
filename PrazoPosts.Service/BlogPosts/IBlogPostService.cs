using System;
using System.Collections.Generic;
using PrazoPosts.Dto;

namespace PrazoPosts.Service.BlogPosts
{
    public interface IBlogPostService
    {
        void CreateBlogPost(string userId, BlogPostDTO blogPostData);
        void UpdateBlogPost(string userId, string _id, BlogPostDTO blogPostData);
        BlogPostDTO GetBlogPost(string userId, string _id);
        IList<BlogPostDTO> GetBlogPosts(string userId);
        void DeleteBlogPost(string userId, string _id);
    }
}
