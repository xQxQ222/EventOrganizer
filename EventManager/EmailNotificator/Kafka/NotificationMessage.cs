namespace EmailNotificator.Kafka
{
    public class NotificationMessage
    {
        public string Email { get; set; }
        public string Event { get; set; }
        public string Status { get; set; }

        public string RequestId { get; set; }
    }
}