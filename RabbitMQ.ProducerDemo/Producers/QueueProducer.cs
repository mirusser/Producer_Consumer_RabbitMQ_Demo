using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ.ProducerDemo.Producers
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel, string queueName = "demo-queue")
        {
            channel.QueueDeclare(
                queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(
                    exchange: "", 
                    routingKey: queueName, 
                    basicProperties: null, 
                    body: body);
                    
                count++;

                Thread.Sleep(1000);
            }
        }
    }
}