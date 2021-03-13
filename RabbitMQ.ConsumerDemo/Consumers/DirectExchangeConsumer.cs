using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer.Consumers
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel, string exchangeName = "demo-direct-exchange", string queueName = "demo-direct-queue")
        {
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

            channel.QueueDeclare(
                queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(queueName, exchangeName, "account.init");
            channel.BasicQos(prefetchSize: 0, prefetchCount: 10, global: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);

            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}