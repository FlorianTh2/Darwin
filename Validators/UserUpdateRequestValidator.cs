using System.Data;
using darwin.Contracts.V1.Requests;
using darwin.Extensions;
using darwin.Options;
using darwin.Services;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace darwin.Validators;

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