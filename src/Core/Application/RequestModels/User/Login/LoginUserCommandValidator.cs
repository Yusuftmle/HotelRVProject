using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Queries;
using FluentValidation;

namespace Application.RequestModels.User.LoginCommand
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(i => i.Email)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");


            RuleFor(i => i.Password)
                .NotNull()
                .MinimumLength(5)
                .WithMessage("{PropertyName} should at least be six characters");
        }
    }
}
