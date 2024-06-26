using PoliceProject.NotificationService.Interfaces;

namespace PoliceProject.NotificationService.Models
{
    public record ExchangeNotified
    {
        public string ExchangeName { get; init; } = default!;
        public string Value { get; init; } = default!;
    }
}
