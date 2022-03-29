using System.Collections.Immutable;
using Darwin.Contracts.V1.Requests;

namespace Darwin.Services;

public interface IUriService
{
    Uri GetBaseUri();

    Uri GetUri(string apiRoute, string id);

    Uri GetAllUri(string apiRoute, PaginationQuery? pagination = null);

    Uri GetUriWithParameters(string apiRoute, Dictionary<string, string> parameters);
}