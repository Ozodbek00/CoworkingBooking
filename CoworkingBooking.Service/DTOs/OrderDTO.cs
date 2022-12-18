namespace CoworkingBooking.Service.DTOs
{
    public class OrderDTO
    {
        public DateTime StartAt { get; set; }

        public DateTime LeaveAt { get; set; }

        public long UserId { get; set; }

        public long ChairId { get; set; }
    }
}
