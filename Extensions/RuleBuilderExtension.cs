using FluentValidation;

namespace hello_asp_identity.Extensions;

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
}