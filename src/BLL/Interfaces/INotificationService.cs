namespace BLL.Interfaces;

public interface INotificationService
{
    Task Notify(int userId, string message);
}
