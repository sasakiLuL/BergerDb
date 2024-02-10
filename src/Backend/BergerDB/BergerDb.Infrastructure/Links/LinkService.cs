using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Contracts.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BergerDb.Infrastructure.Links;

public class LinkService : ILinkService
{
    private readonly LinkGenerator _linkGenerator;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public LinkService(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }

    public Link Generate(string endpointName, object? routeValues, string rel, string method)
    {
        return new Link(
            _linkGenerator.GetUriByName(
                _httpContextAccessor.HttpContext!,
                endpointName,
                routeValues
            )!, rel, method);
    }
}
