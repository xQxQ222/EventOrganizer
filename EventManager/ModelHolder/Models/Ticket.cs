namespace ModelHolder.Models
{
    /// <summary>
    /// Билет на мероприятие
    /// </summary>
    public class Ticket
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public User Booker { get; set; }
        public int? SeatNumber { get; set; } //Если мероприятие предполагает билеты по местам
    }
}
