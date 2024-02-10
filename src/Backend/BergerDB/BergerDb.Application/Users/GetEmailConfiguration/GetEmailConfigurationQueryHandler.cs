using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Users.Responses;
using BergerDb.Domain.Core.Abstractions;
using BergerDb.Domain.Core.Errors;
using BergerDb.Domain.Core.Primitives.Result;
using BergerDb.Domain.Users.EmailConfigurations;
using Mapster;

namespace BergerDb.Application.Users.GetEmailConfiguration;

public class GetEmailConfigurationQueryHandler : IQueryHandler<GetEmailConfigurationQuery, EmailConfigurationResponse>
{
    private IEmailConfigurationRepository _emailConfigurationsRepository;

    public GetEmailConfigurationQueryHandler(IUnitOfWork unitOfWork, IEmailConfigurationRepository emailConfigurationsRepository)
    {
        _emailConfigurationsRepository = emailConfigurationsRepository;
    }

    public async Task<Result<EmailConfigurationResponse>> Handle(GetEmailConfigurationQuery request, CancellationToken cancellationToken)
    {
        EmailConfiguration? emailConfiguration = await _emailConfigurationsRepository.GetEmailConfigurationByUserIdAsync(request.Id);

        if (emailConfiguration is null)
        {
            return Result.Failure<EmailConfigurationResponse>(DomainErrors.User.NotFound);
        }

        var response = emailConfiguration.Adapt<EmailConfigurationResponse>();

        return Result.Success(response);
    }
}
