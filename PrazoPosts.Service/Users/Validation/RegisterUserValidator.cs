using System;
using FluentValidation;
using PrazoPosts.Dto;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Service.Users.Validation
{
    public class RegisterUserValidator :  AbstractValidator<UserDTO>
    {
        IUserRepository _userRepository;
        public RegisterUserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");
            RuleFor(x => x.Email).Must(BeUniqueEmail).WithMessage("Email já existente");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Senha é obrigatória");
            RuleFor(x => x.PasswordConfirmation).NotEmpty().Equal(x => x.Password).WithMessage("Confirmação de senha não confere");
        }

        bool BeUniqueEmail(string email)
        {
            return _userRepository.GetByEmail(email) == null;
        }
    }
}
