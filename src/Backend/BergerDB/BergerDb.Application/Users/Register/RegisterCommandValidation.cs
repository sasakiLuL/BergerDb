using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Users.Register;

public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidation()
    {
        RuleFor(c => c.UserName)
            .NotEmpty()
                .WithError(ValidationErrors.User.UserNameIsRequired);

        RuleFor(c => c.Email)
            .NotEmpty()
                .WithError(ValidationErrors.User.EmailIsRequired);

        RuleFor(c => c.Password)
            .NotEmpty()
                .WithError(ValidationErrors.User.PasswordIsRequired);

        RuleFor(c => c.Roles)
            .NotEmpty()
                .WithError(ValidationErrors.User.RoleIsRequired)
            .Must(r =>
            {
                if (r is null)
                {
                    return true;
                }
                if (r.Count() == 0)
                {
                    return true;
                }
                return r.Distinct().Count() == r.Count();
            })
                .WithError(ValidationErrors.User.DuplicateRoles);
    }
}
