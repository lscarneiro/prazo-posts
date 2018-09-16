using System;
using FluentValidation;
using PrazoPosts.Dto;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Service.Auth.Validation
{
    public class AuthDataValidator : AbstractValidator<AuthDTO>
    {
        public AuthDataValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Senha é obrigatória");
        }
    }
}
