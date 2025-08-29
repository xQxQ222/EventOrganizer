using Confluent.Kafka;
using EmailNotificator.Gmail;
using System.Text.Json;

namespace EmailNotificator.Kafka
{
    public class KafkaConsumer
    {
        private readonly GmailEmailSender emailHandler;

        public KafkaConsumer(GmailEmailSender emailHandler)
        {
            this.emailHandler = emailHandler;
        }

        public async Task StartAsync()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "event-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(new[] { "event.manager.requests.decline", "event.manager.requests.accept" });

            Console.WriteLine("Listening on Kafka topics: event.manager.requests.decline, event.manager.requests.accept...");

            try
            {
                while (true)
                {
                    var cr = consumer.Consume();

                    var notification = JsonSerializer.Deserialize<NotificationMessage>(cr.Message.Value);
                    string message = string.Empty;
                    string messageTitle = string.Empty;
                    if (cr.Topic.Equals("event.manager.requests.decline"))
                    {
                        messageTitle = $"Заявка №{notification.RequestId} отклонена";
                        message = $"Сожалеем. Ваша заявка №{notification.RequestId} на событие \"{notification.Event}\" отклонена.";
                    }
                    else if (cr.Topic.Equals("event.manager.requests.accept"))
                    {
                        messageTitle = $"Заявка №{notification.RequestId} одобрена";
                        message = $"Поздравляем! Ваша заявка №{notification.RequestId} на событие \"{notification.Event}\" одобрена.";
                    }
                    await emailHandler.SendEmailAsync(notification.Email, messageTitle, message, "eventmanager18organizer@gmail.com");
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Dispose();
                consumer.Close();
            }
        }
    }
}
