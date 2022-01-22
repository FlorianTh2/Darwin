using System.Data;
using FluentValidation;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Extensions;
using hello_asp_identity.Options;
using hello_asp_identity.Services;
using Microsoft.Extensions.Options;

namespace hello_asp_identity.Validators;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    private readonly IDateTimeService _dateTimeService;

    public UserUpdateRequestValidator(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;

        RuleFor(a => a.DOB)
            .NotEmpty()
            .WithMessage("dob_required")
            .ValidateDateFormat_UTC_ISO8601(_dateTimeService.Now);

    }

}