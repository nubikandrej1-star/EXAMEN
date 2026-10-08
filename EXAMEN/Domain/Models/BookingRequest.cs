using EXAMEN.DTOs.Requests;

namespace EXAMEN.Domain.Models
{
    public class BookingRequest
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime PreferredDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public Car? Car { get; set; }


        public void UpdateBookingRequest(CreateBookingDtoRequest bookingRequest)
        {
            CarId = bookingRequest.CarId;
            CustomerName = bookingRequest.CustomerName;
            CustomerPhone = bookingRequest.CustomerPhone;
            CustomerEmail = bookingRequest.CustomerEmail;
            PreferredDate = bookingRequest.PreferredDate;
            Status = "Pending";
        }
    }
}
