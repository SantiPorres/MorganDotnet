using Application.Interfaces.ServicesInterfaces;

namespace Application.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUTC => DateTime.UtcNow;
    }
}
