using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMq.Consumer.Consumers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer.Consumers;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            
            // QueueConsumer.Consume(channel);
            // DirectExchangeConsumer.Consume(channel);
            // TopicExchangeConsumer.Consume(channel);
            // HeaderExchangeConsumer.Consume(channel);
            FanoutExchangeConsumer.Consume(channel);
        }
    }
}
