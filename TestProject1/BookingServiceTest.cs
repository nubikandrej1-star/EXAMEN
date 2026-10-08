using EXAMEN.Data.DBContext;
using EXAMEN.Domain.Models;
using EXAMEN.DTOs.Requests;
using EXAMEN.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using Xunit;
//vggf
namespace UnitTest1
{
    public class BookingServiceTests
    {
        private CarDalershipDB CreateContext()
        {
            var options = new DbContextOptionsBuilder<CarDalershipDB>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CarDalershipDB(options);
        }

        [Fact]
        public async Task CreateBookingRequestAsync_ShouldCreateBooking_WhenCarIsAvailable()
        {
            // Arrange
            using var context = CreateContext();

            var car = new Car
            {
                Id = 1,
                IsAvailable = true
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var service = new BookingService(context);

            var request = new CreateBookingDtoRequest
            {
                CarId = 1
            };

            // Act
            var result = await service.CreateBookingRequestAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CarId);

            var bookingExists = await context.BookingRequests
                .AnyAsync(x => x.CarId == 1);

            Assert.True(bookingExists);
        }

        [Fact]
        public async Task CreateBookingRequestAsync_ShouldThrowException_WhenCarIsUnavailable()
        {
            // Arrange
            using var context = CreateContext();

            var car = new Car
            {
                Id = 1,
                IsAvailable = false
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var service = new BookingService(context);

            var request = new CreateBookingDtoRequest
            {
                CarId = 1
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => service.CreateBookingRequestAsync(request));

            Assert.Equal(
                "Автомобіль недоступний для тест-драйву.",
                exception.Message);
        }
    }
}