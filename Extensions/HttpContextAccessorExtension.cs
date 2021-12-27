namespace hello_asp_identity.Extensions;

public static class HttpContextAccessorExtension
{
    public static string GetCurrentAbsolutUri(IHttpContextAccessor accessor)
    {
        var request = accessor
            .HttpContext?
            .Request;
        var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
        return absoluteUri;
    }
}