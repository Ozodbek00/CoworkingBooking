using CoworkingBooking.Domain.Commons;
using CoworkingBooking.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoworkingBooking.Domain.Entities
{
    public class User : Auditable
    {
        [MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string LastName { get; set; }

        [MaxLength(14)]
        public string PhoneNumber { get; set; }

        [MaxLength(16)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public UserRole UserRole { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
