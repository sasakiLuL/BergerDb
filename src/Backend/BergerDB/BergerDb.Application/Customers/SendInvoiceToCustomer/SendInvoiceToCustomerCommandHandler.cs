using BergerDb.Application.Core.Abstractions.Email;
using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Customers;
using BergerDb.Domain.Customers.Memberships;

namespace BergerDb.Application.Customers.SendEmailToCustomer;

public class SendInvoiceToCustomerCommandHandler : ICommandHandler<SendInvoiceToCustomerCommand>
{
    private ICustomerRepository _customerRepository;

    private IMembershipRepository _membershipRepository;

    private IEmailService _emailService;

    private IUnitOfWork _unitOfWork;

    public SendInvoiceToCustomerCommandHandler(ICustomerRepository customerRepository, IEmailService emailService, IMembershipRepository membershipRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _emailService = emailService;
        _membershipRepository = membershipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SendInvoiceToCustomerCommand request, CancellationToken cancellationToken)
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
            request.FilePdf));

        var membership = await _membershipRepository.GetMembershipByCustomerIdAsync(request.Id);

        membership!.InvoiceIsSended();

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
