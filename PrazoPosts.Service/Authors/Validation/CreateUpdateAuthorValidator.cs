using System;
using FluentValidation;
using PrazoPosts.Dto;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Service.Authors.Validation
{
    public class CreateUpdateAuthorValidator : AbstractValidator<AuthorDTO>
    {
        public CreateUpdateAuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
        }
    }
}
