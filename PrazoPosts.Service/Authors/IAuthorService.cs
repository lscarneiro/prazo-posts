using System;
using System.Collections.Generic;
using PrazoPosts.Dto;

namespace PrazoPosts.Service.Authors
{
    public interface IAuthorService
    {
        void CreateAuthor(AuthorDTO authorData);
        AuthorDTO GetAuthor(string _id);
        IList<AuthorDTO> GetAuthors();
        void DeleteAuthor(string _id);
    }
}
