using BergerDb.Application.Core.Abstractions.Messaging;
using BergerDb.Contracts.Users.Responses;

namespace BergerDb.Application.Users.GetEmailConfiguration;

public record GetEmailConfigurationQuery(
    Guid Id) : IQuery<EmailConfigurationResponse>;
