using CoworkingBooking.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingBooking.Domain.Entities
{
    public class Branch : Auditable
    {
        public string Name { get; set; } = String.Empty;
    }
}
