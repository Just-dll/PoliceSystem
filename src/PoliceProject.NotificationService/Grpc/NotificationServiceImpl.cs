using Grpc.Core;
using PoliceProject.NotificationService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace PoliceProject.NotificationService.Grpc
{
    public class NotificationServiceImpl : NotificationService.NotificationServiceBase
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public NotificationServiceImpl(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
        }

        public override async Task<NotificationResponse> Notify(Notification request, ServerCallContext context)
        {
            await Task.Run(() => _channel.BasicPublish(request.ExchangeName, string.Empty, null, Encoding.UTF8.GetBytes(request.Value)));
            return new();
        }

        public override async Task<NotificationResponse> ConnectPersonToExchange(Connect request, ServerCallContext context)
        {
            try
            {
                var queueName = $"user_{request.PersonId.ToString()}";
                _channel.QueueDeclare(queue: queueName,
                                      durable: true,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);
                await Task.Run(() => _channel.QueueBind(queueName, request.ExchangeName, string.Empty));
                return new();
            }
            catch (OperationInterruptedException ex)
            {
                Console.WriteLine(ex.Message);
                return new();
            }
        }
    }
}
