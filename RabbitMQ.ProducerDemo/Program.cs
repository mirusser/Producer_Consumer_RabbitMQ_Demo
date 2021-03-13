using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMq.ProducerDemo.Producers;
using RabbitMQ.Client;
using RabbitMQ.ProducerDemo.Producers;

namespace RabbitMQ.ProducerDemo
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

            // QueueProducer.Publish(channel);
            // DirectExchangePublisher.Publish(channel);
            // TopicExchangePublisher.Publish(channel);
            // HeaderExchangePublisher.Publish(channel);
            FanoutExchangePublisher.Publish(channel);
        }
    }
}
