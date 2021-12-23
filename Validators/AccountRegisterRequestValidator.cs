using System.Data;
using FluentValidation;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Extensions;

namespace hello_asp_identity.Validators;

public class AccountRegisterRequestValidator : AbstractValidator<AccountRegisterRequest>
{
    public AccountRegisterRequestValidator()
    {
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
            .Password();

        RuleFor(a => a.PasswordConfirm)
            .NotEmpty()
            .WithMessage("password-confirmation_required")
            .Equal(a => a.Password)
            .WithMessage("password-confirmation_invalid");
    }
}