using CoworkingBooking.Domain.Commons;

namespace CoworkingBooking.Domain.Entities
{
    public sealed class Floor : Auditable
    {
        public short Index { get; set; }

        public long BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
