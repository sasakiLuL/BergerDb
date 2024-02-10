using BergerDb.Application.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Application.Users.UpdateUser;

public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidation()
    {
        RuleFor(c => c.UserName)
            .NotEmpty()
                .WithError(ValidationErrors.User.UserNameIsRequired);
    }
}
