namespace hello_asp_identity.Services;

public class DateTimeService : IDateTimeService
{
    DateTime IDateTimeService.Now => DateTime.UtcNow;
}