using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq.Consumer.Consumers
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel, string exchangeName = "demo-header-exchange", string queueName = "demo-header-queue")
        {
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Headers);

            channel.QueueDeclare(
                queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var header = new Dictionary<string, object> { { "account", "new"} };

            channel.QueueBind(queueName, exchangeName, string.Empty, header);
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