using System.Data;
using Darwin.Contracts.V1.Requests;
using Darwin.Extensions;
using Darwin.Options;
using Darwin.Services;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Darwin.Validators;

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