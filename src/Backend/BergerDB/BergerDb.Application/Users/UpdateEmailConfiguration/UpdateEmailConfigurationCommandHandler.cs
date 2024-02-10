using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Users.EmailConfigurations;

namespace BergerDb.Application.Users.UpdateEmailConfiguration;

public class UpdateEmailConfigurationCommandHandler : ICommandHandler<UpdateEmailConfigurationCommand>
{
    private IEmailConfigurationRepository _emailConfigurationRepository;

    private IUnitOfWork _unitOfWork;

    public UpdateEmailConfigurationCommandHandler(IEmailConfigurationRepository emailConfigurationRepository, IUnitOfWork unitOfWork)
    {
        _emailConfigurationRepository = emailConfigurationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateEmailConfigurationCommand request, CancellationToken cancellationToken)
    {
        EmailConfiguration? emailConfiguration =
            await _emailConfigurationRepository.GetEmailConfigurationByUserIdAsync(request.Id);

        if (emailConfiguration is null)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        emailConfiguration.Update(
            request.Street,
            request.City,
            request.ZipCode,
            request.PhoneNumber,
            request.Email,
            request.HomePage,
            request.AccountName,
            request.IBAN,
            request.BIC,
            request.GID,
            request.TaxIdentificationNumber,
            request.InvoicePdfBody,
            request.InvoiceEmailSubject,
            request.InvoiceEmailBody,
            request.BillingRemindingEmailSubject,
            request.BillingRemindingEmailBody,
            request.DirectDebitingRemindingEmailSubject,
            request.DirectDebitingRemindingEmailBody);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
