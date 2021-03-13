using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq.Consumer.Consumers
{
    public static class TopicExchangeConsumer
    {
        public static void Consume(IModel channel, string exchangeName = "demo-topic-exchange", string queueName = "demo-topic-queue")
        {
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

            channel.QueueDeclare(
                queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(queueName, exchangeName, "account.*");
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