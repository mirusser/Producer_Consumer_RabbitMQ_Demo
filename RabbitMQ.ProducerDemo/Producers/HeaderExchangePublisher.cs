using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMq.ProducerDemo.Producers
{
    public static class HeaderExchangePublisher
    {
        public static void Publish(IModel channel, string exchangeName = "demo-header-exchange")
        {
            var ttl = new Dictionary<string, object>
            {
                //ttl: time to live in ms (30000ms = 30s)
                { "x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Headers, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> {{"account", "new"}};

                channel.BasicPublish(
                    exchange: exchangeName,
                    routingKey: string.Empty,
                    basicProperties: properties,
                    body: body);

                count++;

                Thread.Sleep(1000);
            }
        }
    }
}