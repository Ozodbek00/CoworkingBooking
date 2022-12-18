using CoworkingBooking.Domain.Enums;

namespace CoworkingBooking.Service.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserRole UserRole { get; set; }
    }
}
