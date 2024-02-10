namespace BergerDb.Application.Core.Abstractions.Responses;

public interface IResponseLinksService<TResponse>
{
    void GenerateLinks(TResponse response);
}
