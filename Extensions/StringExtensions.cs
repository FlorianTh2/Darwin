namespace Darwin.Extensions;

public static class StringExtensions
{
    public static DateTime FromIso8601StringToDateTime(this string a)
    {
        return DateTime.Parse(a, null, System.Globalization.DateTimeStyles.RoundtripKind);
    }
}