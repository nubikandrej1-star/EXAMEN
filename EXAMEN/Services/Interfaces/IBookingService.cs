using EXAMEN.Domain.Models;
using EXAMEN.DTOs.Requests;

namespace EXAMEN.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingRequest> CreateBookingRequestAsync(CreateBookingDtoRequest bookingRequest);
        Task<IEnumerable<BookingRequest>> GetAllBookingRequestsAsync();
        Task<BookingRequest> GetBookingRequestByIdAsync(int id);

        /*
        Task<BookingRequest?> GetBookingRequestByIdAsync(int id);
        Task<bool> DeleteBookingRequestAsync(int id);
        */
    }
}
