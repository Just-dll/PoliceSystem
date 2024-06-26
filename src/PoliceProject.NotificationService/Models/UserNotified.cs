using PoliceProject.NotificationService.Interfaces;

namespace PoliceProject.NotificationService.Models
{
    public record UserNotified
    {
        public int UserId { get; init; } = default!;
        public string Value { get; init; } = default!;
    }
}