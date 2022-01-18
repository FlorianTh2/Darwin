using hello_asp_identity.Contracts.V1.Requests;

namespace hello_asp_identity.Services;

public interface IUriService
{
    Uri GetUri(string apiRoute, string id);

    Uri GetAllUri(string apiRoute, PaginationQuery pagination = null);
}