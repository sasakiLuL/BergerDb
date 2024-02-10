using BergerDb.Application.Core.Abstractions.Email;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers.Memberships;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Core.Errors;

namespace BergerDb.Application.Customers.SendDunningToCustomer;

public class SendDunningToCustomerCommandHandler : ICommandHandler<SendDunningToCustomerCommand>
{
    private ICustomerRepository _customerRepository;

    private IMembershipRepository _membershipRepository;

    private IEmailService _emailService;

    private IUnitOfWork _unitOfWork;

    public SendDunningToCustomerCommandHandler(ICustomerRepository customerRepository, IEmailService emailService, IMembershipRepository membershipRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _emailService = emailService;
        _membershipRepository = membershipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SendDunningToCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.Id);

        if (customer is null)
        {
            return Result.Failure(DomainErrors.Customer.NotFound);
        }

        var customerEmail = customer.Email;

        await _emailService.SendEmailAsync(new(
            customerEmail,
            request.Subject,
            request.BodyText,
            request.FileName,
            request.PdfFile));

        var membership = await _membershipRepository.GetMembershipByCustomerIdAsync(request.Id);

        membership!.DunningIsSended();

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
