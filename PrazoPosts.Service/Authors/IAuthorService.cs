using System;
using System.Collections.Generic;
using PrazoPosts.Dto;

namespace PrazoPosts.Service.Authors
{
    public interface IAuthorService
    {
        void CreateAuthor(string userId, AuthorDTO authorData);
        void UpdateAuthor(string userId, string id, AuthorDTO authorData);
        AuthorDTO GetAuthor(string userId, string _id);
        IList<AuthorDTO> GetAuthors(string userId);
        void DeleteAuthor(string userId, string _id);
    }
}
