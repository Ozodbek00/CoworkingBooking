using CoworkingBooking.Domain.Commons;

namespace CoworkingBooking.Domain.Entities
{
    public sealed class Chair : Auditable
    {
        public int Index { get; set; }

        public double Price { get; set; }
        
        public long TableId { get; set; }
        public Table Table { get; set; }
    }
}
