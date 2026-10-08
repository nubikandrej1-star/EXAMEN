using EXAMEN.Domain.Models;

namespace EXAMEN.DTOs.Requests
{
    public class CreateBookingDtoRequest
    {
        public int CarId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime PreferredDate { get; set; }
    }
}
