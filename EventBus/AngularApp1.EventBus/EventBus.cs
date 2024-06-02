using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AngularApp1.EventBus
{
    public class EventBus : IEventBus, IDisposable // Scoped
    {
        private readonly IConnection _connection;
        private readonly IModel model;
        
        public EventBus()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            model = _connection.CreateModel();
        }

        public Task ConnectPerson(string personId, string exchange)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            model.Dispose();
            _connection.Dispose();
        }

        public Task<IEnumerable<string>> GetMessages(string queueName)
        {
            model.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var consumer = new EventingBasicConsumer(model);
            var list = new List<string>();
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                list.Add(message);
                Console.WriteLine($" [x] Received {message}");
            };
            model.BasicConsume(queueName, true, consumer);

            return Task.FromResult(list.AsEnumerable());
        }

        public Task Publish(string queueName, string message) // Think about posting to topic.
        {
            var model = _connection.CreateModel();

            model.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            model.BasicPublish(exchange: string.Empty,
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}
