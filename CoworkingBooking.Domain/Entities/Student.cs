using CoworkingBooking.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingBooking.Domain.Entities
{
    public class Student : Auditable
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
