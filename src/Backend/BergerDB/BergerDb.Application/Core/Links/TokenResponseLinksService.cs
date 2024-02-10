using BergerDb.Api.Contracts;
using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Users.Responses;

namespace BergerDb.Application.Core.Links;

public class TokenResponseLinksService : ITokenResponseLinksService
{
    private readonly ILinkService _linkService;

    public TokenResponseLinksService(ILinkService linkService)
    {
        _linkService = linkService;
    }

    public void GenerateLinks(TokenResponse response)
    {
        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Users.GetById,
                new { response.Id },
                "get-user",
                "GET"));
    }
}
