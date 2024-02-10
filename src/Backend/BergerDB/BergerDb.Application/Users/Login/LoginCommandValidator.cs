using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Users.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
                .WithError(ValidationErrors.Login.EmailIsRequired);

        RuleFor(c => c.Password)
            .NotEmpty()
                .WithError(ValidationErrors.Login.PasswordIsRequired);
    }
}
