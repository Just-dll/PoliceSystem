using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Client;
using System.Text;

public class NotificationService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private TaskCompletionSource<string?> _tcs = new TaskCompletionSource<string?>();

    public NotificationService(IConnection connection)
    {
        _connection = connection;
        _channel = _connection.CreateModel();
    }

    public async Task Publish(string exchangeName, string message)
    {
        _channel.BasicPublish(exchangeName, string.Empty, null, Encoding.UTF8.GetBytes(message));
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
        return _tcs.Task;
    }

    public void CreateExchange(string exchangeName)
    {
        _channel.ExchangeDeclare(exchange: exchangeName,
                                 durable: true,
                                 type: ExchangeType.Fanout);
    }

    public async Task ConnectToExchange(string queueName, string exchange)
    {
        try
        {
            _channel.QueueDeclare(queue: queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            await Task.Run(() => _channel.QueueBind(queueName, exchange, string.Empty));
        }
        catch (OperationInterruptedException ex)
        {
            // Handle the exception as needed
            Console.WriteLine(ex.Message);
        }
    }

    public void Reset()
    {
        _tcs = new TaskCompletionSource<string?>();
    }

    public void Close()
    {
        _channel.Close();
    }
}
