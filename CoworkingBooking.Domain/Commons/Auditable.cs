using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingBooking.Domain.Commons
{
    public abstract class Auditable
    {
        public long Id { get; set; }

    }
}
