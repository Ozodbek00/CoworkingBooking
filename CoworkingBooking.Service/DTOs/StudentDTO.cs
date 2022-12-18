using CoworkingBooking.Domain.Enums;

namespace CoworkingBooking.Service.DTOs
{
    public class StudentDTO
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public UserRole UserRole { get; set; }
    }
}
