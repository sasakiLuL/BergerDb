using BergerDb.Api.Contracts;
using BergerDb.Application.Core.Abstractions.Links;
using BergerDb.Application.Core.Abstractions.Responses;
using BergerDb.Contracts.Users.Responses;

namespace BergerDb.Application.Core.Links;

public class UserResponseLinksService : IUserResponseLinksService
{
    private readonly ILinkService _linkService;

    public UserResponseLinksService(ILinkService linkService)
    {
        _linkService = linkService;
    }

    public void GenerateLinks(UserResponse response)
    {
        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Users.GetById,
                new { response.Id },
                "self",
                "GET"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Users.Update,
                new { response.Id },
                "update-user",
                "PUT"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Users.ChangePassword,
                new { response.Id },
                "change-user-password",
                "PUT"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Users.Delete,
                new { response.Id },
                "delete-user",
                "DELETE"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Users.GetEmailConfiguration,
                new { response.Id },
                "get-user-email-configuration",
                "GET"));

        response.Links.Add(
            _linkService.Generate(
                EndpointNames.Users.UpdateEmailConfiguration,
                new { response.Id },
                "update-user-email-configuration",
                "PUT"));
    }
}
