using CoworkingBooking.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingBooking.Domain.Entities
{
    public class Table : Auditable
    {
        public int Index { get; set; }
        public double Price { get; set; }
        public long FloorId { get; set; }
        public Floor Floor { get; set; }
    }
}
