using System;
using FluentValidation;
using PrazoPosts.Dto;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Service.Authors.Validation
{
    public class CreateAuthorValidator : AbstractValidator<AuthorDTO>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
        }
    }
}
