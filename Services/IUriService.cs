using System.Collections.Immutable;
using hello_asp_identity.Contracts.V1.Requests;

namespace hello_asp_identity.Services;

public interface IUriService
{
    Uri GetBaseUri();

    Uri GetUri(string apiRoute, string id);

    Uri GetAllUri(string apiRoute, PaginationQuery? pagination = null);

    Uri GetUriWithParameters(string apiRoute, Dictionary<string, string> parameters);
}