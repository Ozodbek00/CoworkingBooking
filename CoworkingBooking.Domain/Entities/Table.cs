using CoworkingBooking.Domain.Commons;

namespace CoworkingBooking.Domain.Entities
{
    public class Table : Auditable
    {
        public int Index { get; set; }

        public double Price { get; set; }

        public long FloorId { get; set; }
        public Floor Floor { get; set; }

        public virtual ICollection<Chair> Chairs { get; set; }
    }
}
