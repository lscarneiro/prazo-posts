using System;
using FluentValidation;
using PrazoPosts.Dto;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Service.BlogPosts.Validation
{
    public class CreateUpdateBlogPostValidator : AbstractValidator<BlogPostDTO>
    {
        public CreateUpdateBlogPostValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Autor é obrigatório");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Título é obrigatório");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Conteúdo é obrigatório");
        }
    }
}
