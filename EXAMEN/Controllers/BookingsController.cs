using EXAMEN.DTOs.Requests;
using EXAMEN.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EXAMEN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(
            [FromBody] CreateBookingDtoRequest bookingRequest)
        {
            try
            {
                var booking = await _bookingService
                    .CreateBookingRequestAsync(bookingRequest);

                return CreatedAtAction(
                    nameof(GetBookingById),
                    new { id = booking.Id },
                    booking);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookingService
                .GetAllBookingRequestsAsync();

            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingService
                    .GetBookingRequestByIdAsync(id);

                return Ok(booking);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }
    }
}