using System.Globalization;
using darwin.Services;
using FluentValidation;

namespace darwin.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> a, int minimumLength = 14)
    {
        var options = a
            .NotEmpty().WithMessage("password_required")
            .MinimumLength(minimumLength).WithMessage("password_invalid")
            .Matches("[A-Z]").WithMessage("password_invalid")
            .Matches("[a-z]").WithMessage("password_invalid")
            .Matches("[0-9]").WithMessage("password_invalid")
            .Matches("[^a-zA-Z0-9]").WithMessage("password_invalid");
        return options;
    }

    public static IRuleBuilder<T, string> ValidateDateFormat_UTC_ISO8601<T>(this IRuleBuilder<T, string> a, DateTime currentDate)
    {
        return a
        .Must(a =>
        {
            // https://stackoverflow.com/questions/60106502/how-to-validate-check-datetime-input-as-iso-8601
            DateTime result = new DateTime();
            return DateTime.TryParseExact(
                a,
                // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings?redirectedfrom=MSDN#Roundtrip
                "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK",
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind,
                out result
            );
        })
        .WithMessage("dob_notParsable")
        .Must(a =>
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            var timeSpan = currentDate - a.FromIso8601StringToDateTime();
            // Because we start at year 1 for the Gregorian
            // calendar, we must subtract a year here.
            int years = (zeroTime + timeSpan).Year - 1;
            return years > 12 && years < 100;
        })
        .WithMessage("dob_notViable");
    }
}