using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Users.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.Password)
            .NotEmpty()
                .WithError(ValidationErrors.User.PasswordIsRequired);
    }
}
