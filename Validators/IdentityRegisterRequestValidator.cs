using System.Data;
using FluentValidation;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Extensions;
using hello_asp_identity.Options;
using hello_asp_identity.Services;
using Microsoft.Extensions.Options;

namespace hello_asp_identity.Validators;

public class IdentityRegisterRequestValidator : AbstractValidator<IdentityRegisterRequest>
{
    private readonly AccountSecruityOptions _accountSecruityOptions;
    private readonly IDateTimeService _dateTimeService;

    public IdentityRegisterRequestValidator(
        IOptions<AccountSecruityOptions> accountSecruityOptions,
        IDateTimeService dateTimeService)
    {
        _accountSecruityOptions = accountSecruityOptions.Value;
        _dateTimeService = dateTimeService;

        RuleFor(a => a.Username)
            .NotEmpty()
            .WithMessage("username_required");

        RuleFor(a => a.Email)
            .NotEmpty()
            .WithMessage("email_required")
            .EmailAddress()
            .WithMessage("email_invalid");

        RuleFor(a => a.DOB)
            .NotEmpty()
            .WithMessage("dob_required")
            .ValidateDateFormat_UTC_ISO8601(_dateTimeService.Now);

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