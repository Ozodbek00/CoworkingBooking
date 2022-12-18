using CoworkingBooking.Domain.Commons;

namespace CoworkingBooking.Domain.Entities
{
    public sealed class Order : Auditable
    {
        public DateTime StartAt { get; set; }

        public DateTime LeaveAt { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
        
        public long ChairId { get; set; }
        public Chair Chair { get; set; }
    }
}
