using CoworkingBooking.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace CoworkingBooking.Domain.Entities
{
    public sealed class Branch : Auditable
    {
        [MaxLength(32)]
        public string Name { get; set; }
    }
}
