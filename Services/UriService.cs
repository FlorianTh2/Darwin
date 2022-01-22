using System.Text.RegularExpressions;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace hello_asp_identity.Services;

public class UriService : IUriService
{
    private readonly string _baseUri;

    public UriService(string baseUri)
    {
        _baseUri = baseUri;
    }
    public Uri GetBaseUri()
    {
        return new Uri(_baseUri);
    }

    public Uri GetUri(string apiRoute, string id)
    {
        return new Uri(_baseUri + Regex.Replace(apiRoute, "{.*?}", id));
    }

    public Uri GetAllUri(string apiRoute, PaginationQuery? pagination = null)
    {
        var uri = new Uri(_baseUri + apiRoute);

        if (pagination == null)
        {
            return uri;
        }

        var modifiedUri = QueryHelpers.AddQueryString(uri.ToString(), "pageNumber", pagination.PageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
        return new Uri(modifiedUri);
    }
}