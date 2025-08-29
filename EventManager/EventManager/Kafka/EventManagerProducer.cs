using Confluent.Kafka;
using ModelHolder.Models;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace EventManager.Kafka
{
    public class EventManagerProducer
    {
        private readonly IProducer<Null, string> producer;

        public EventManagerProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendBookingSuccessAsync(string email, string requestId, string eventName)
        {
            var message = new
            {
                Email = email,
                Event = eventName,
                Status = "CONFIRMED",
                RequestId = requestId
            };

            var payload = JsonSerializer.Serialize(message);

            await producer.ProduceAsync("event.manager.requests.accept",
                new Message<Null, string> { Value = payload });
        }

        public async Task SendBookingDeclineAsync(string email, string requestId, string eventName)
        {
            var message = new
            {
                Email = email,
                Event = eventName,
                Status = "CONFIRMED",
                RequestId = requestId
            };

            var payload = JsonSerializer.Serialize(message);

            await producer.ProduceAsync("event.manager.requests.decline",
                new Message<Null, string> { Value = payload });
        }
    }
}
