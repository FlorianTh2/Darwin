using System.Data;
using FluentValidation;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Extensions;
using hello_asp_identity.Options;
using Microsoft.Extensions.Options;

namespace hello_asp_identity.Validators;

public class IdentityRegisterRequestValidator : AbstractValidator<IdentityRegisterRequest>
{
    private readonly AccountSecruityOptions _accountSecruityOptions;

    public IdentityRegisterRequestValidator(IOptions<AccountSecruityOptions> accountSecruityOptions)
    {
        _accountSecruityOptions = accountSecruityOptions.Value;

        RuleFor(a => a.UserName)
            .NotEmpty()
            .WithMessage("username_required");

        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("email_required")
            .EmailAddress()
            .WithMessage("email_invalid");

        RuleFor(a => a.Password)
            .NotEmpty()
            .WithMessage("password_required")
            .Password(minimumLength: _accountSecruityOptions.PasswordLength);

        RuleFor(a => a.PasswordConfirm)
            .NotEmpty()
            .WithMessage("password-confirmation_required")
            .Equal(a => a.Password)
            .WithMessage("password-confirmation_invalid");
    }
}