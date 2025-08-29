using EmailNotificator.Gmail;
using EmailNotificator.Kafka;

namespace EmailNotificator;

public class Program
{
    public static void Main(string[] args)
    {
        StartProgram();
    }

    private static async void StartProgram()
    {
        KafkaConsumer consumer = new KafkaConsumer(new GmailEmailSender());
        await consumer.StartAsync();
    }
}
