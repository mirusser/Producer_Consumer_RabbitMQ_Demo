using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMq.ProducerDemo.Producers
{
    public static class TopicExchangePublisher
    {
        public static void Publish(IModel channel, string exchangeName = "demo-topic-exchange")
        {
            var ttl = new Dictionary<string, object>
            {
                //ttl: time to live in ms (30000ms = 30s)
                { "x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(
                    exchange: exchangeName,
                    routingKey: "account.udpate",
                    basicProperties: null,
                    body: body);

                count++;

                Thread.Sleep(1000);
            }
        }
    }
}