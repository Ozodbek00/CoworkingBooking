using CoworkingBooking.Domain.Commons;

namespace CoworkingBooking.Domain.Entities
{
    public sealed class Order : Auditable
    {
        public DateTime StartAt { get; set; }

        public DateTime LeaveAt { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
        
        public long BranchId { get; set; }
        public Branch Branch { get; set; }
        
        public long FloorId { get; set; }
        public Floor Floor { get; set; }
        
        public long TableId { get; set; }
        public Table Table { get; set; }
        
        public long ChairId { get; set; }
        public Chair Chair { get; set; }
    }
}
