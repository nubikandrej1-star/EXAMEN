using EXAMEN.Data.DBContext;
using EXAMEN.Domain.Models;
using EXAMEN.DTOs.Requests;
using EXAMEN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EXAMEN.Domain.Validators;

namespace EXAMEN.Services
{
    public class BookingService : IBookingService
    {
        private readonly CarDalershipDB _context;

        public BookingService(CarDalershipDB context)
        {
            _context = context;
        }

        public async Task<BookingRequest> CreateBookingRequestAsync(CreateBookingDtoRequest createBookingRequest)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(c => c.Id == createBookingRequest.CarId);

            CreateBookingDtoValidator validator = new CreateBookingDtoValidator();
            var validationResult = validator.Validate(createBookingRequest);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Невірні дані для створення запиту на бронювання." + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            if (car == null)
            {
                throw new KeyNotFoundException("Автомобіль не знайдено.");
            }

            if (!car.IsAvailable)
            {
                throw new InvalidOperationException(
                    "Автомобіль недоступний для тест-драйву.");
            }

            BookingRequest bookingRequest = new BookingRequest();
            bookingRequest.UpdateBookingRequest(createBookingRequest);

            _context.BookingRequests.Add(bookingRequest);
            await _context.SaveChangesAsync();

            return bookingRequest;
        }

        public async Task<IEnumerable<BookingRequest>> GetAllBookingRequestsAsync()
        {
            return await _context.BookingRequests
                .Include(br => br.Car)
                .ToListAsync();
        }

        public async Task<BookingRequest> GetBookingRequestByIdAsync(int id)
        {
            var bookingRequest = await _context.BookingRequests
                .Include(br => br.Car)
                .FirstOrDefaultAsync(br => br.Id == id);
            if (bookingRequest == null)
            {
                throw new KeyNotFoundException("Запит на бронювання не знайдено.");
            }
            return bookingRequest;
        }
    }
}