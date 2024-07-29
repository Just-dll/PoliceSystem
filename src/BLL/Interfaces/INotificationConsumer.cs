using DAL.Entities;

namespace BLL.Interfaces;

public interface INotificationConsumer
{
    Task<string?> Subscribe();
    User UserConsumer { get; set; }
}
