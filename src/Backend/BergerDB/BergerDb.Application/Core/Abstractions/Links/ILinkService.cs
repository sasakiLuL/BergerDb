using BergerDb.Contracts.Common;

namespace BergerDb.Application.Core.Abstractions.Links;

public interface ILinkService
{
    Link Generate(string endpointName, object? routeValues, string rel, string method);
}
