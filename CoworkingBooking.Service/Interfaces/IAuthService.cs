using CoworkingBooking.Domain.Entities;

namespace CoworkingBooking.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
