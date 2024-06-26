using PoliceProject.NotificationService.Grpc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System.Text;

namespace PoliceProject.NotificationService
{
    public class ServerSentNotificationEmitter
    {
        private TaskCompletionSource<string?> _tcs = new TaskCompletionSource<string?>();
        private readonly IModel _channel;
        public ServerSentNotificationEmitter(IConnection connection)
        {
            _channel = connection.CreateModel();
        }

        public void StartConsuming(string queueName) 
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _tcs.TrySetResult(message);
            };
            _channel.QueueDeclare(queue: queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            _channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);
        }

        public Task<string?> WaitForMessage()
        {
            var task = _tcs.Task;
            // _tcs = new TaskCompletionSource<string?>();
            return task;
        }

        public void Reset()
        {
            _tcs = new TaskCompletionSource<string?>();
        }

        public void Close() // Disable or unsubscribe
        {
            _channel.Close();
        }
    }
}
