namespace darwin.Services;

public class DateTimeService : IDateTimeService
{
    DateTime IDateTimeService.Now => DateTime.UtcNow;
}