namespace hello_asp_identity.Extensions;

public static class DateTimeExtensions
{
    public static string ToIso8601String(this DateTime a)
    {
        return a.ToString("o");
    }
}