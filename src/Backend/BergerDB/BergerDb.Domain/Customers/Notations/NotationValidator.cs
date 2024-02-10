using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Extensions;
using FluentValidation;

namespace BergerDb.Domain.Customers.Notations;

public class NotationValidator : AbstractValidator<Notation>
{
    public NotationValidator()
    {
        RuleFor(n => n.Value)
            .MaximumLength(Notation.MaximumLength)
                .WithError(DomainErrors.Notation.TooLong);
    }
}
