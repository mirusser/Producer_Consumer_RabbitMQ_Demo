
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer.Consumers
{
    public static class QueueConsumer
    {
        public static void Consume(IModel channel, string queueName = "demo-queue")
        {
            channel.QueueDeclare(
                queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                System.Console.WriteLine(message);
            };

            channel.BasicConsume(queueName, true, consumer);

            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}