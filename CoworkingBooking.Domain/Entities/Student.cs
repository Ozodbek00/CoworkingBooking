using CoworkingBooking.Domain.Commons;
using CoworkingBooking.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoworkingBooking.Domain.Entities
{
    public sealed class Student : Auditable
    {
        [MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string LastName { get; set; }

        [MaxLength(14)]
        public string PhoneNumber { get; set; }

        public UserRole UserRole { get; set; }
    }
}
