using CoworkingBooking.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingBooking.Domain.Entities
{
    public class Floor : Auditable
    {
        public short Index { get; set; }
        public long BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
