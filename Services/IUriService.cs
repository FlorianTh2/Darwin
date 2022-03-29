using System.Collections.Immutable;
using darwin.Contracts.V1.Requests;

namespace darwin.Services;

public interface IUriService
{
    Uri GetBaseUri();

    Uri GetUri(string apiRoute, string id);

    Uri GetAllUri(string apiRoute, PaginationQuery? pagination = null);

    Uri GetUriWithParameters(string apiRoute, Dictionary<string, string> parameters);
}