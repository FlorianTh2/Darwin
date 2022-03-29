using System.Text.RegularExpressions;
using System.Web;
using darwin.Contracts.V1.Requests;
using darwin.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace darwin.Services;

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
        return new Uri(GetBaseUri(), Regex.Replace(apiRoute, "{.*?}", id));
    }

    public Uri GetAllUri(string apiRoute, PaginationQuery? pagination = null)
    {
        var uri = new Uri(GetBaseUri(), apiRoute);

        if (pagination == null)
        {
            return uri;
        }

        var modifiedUri = QueryHelpers.AddQueryString(uri.ToString(), "pageNumber", pagination.PageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
        return new Uri(modifiedUri);
    }

    public Uri GetUriWithParameters(string apiRoute, Dictionary<string, string> parameters)
    {
        var uriBuilder = new UriBuilder(GetBaseUri());
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        foreach (KeyValuePair<string, string> entry in parameters)
        {
            query[entry.Key] = entry.Value;
        }
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }
}