using EXAMEN.Data.DBContext;
using EXAMEN.Domain.Models;
using EXAMEN.Domain.Plaginations;
using EXAMEN.Domain.Plaginations.EXAMEN.Services;
using EXAMEN.DTOs.Requests;
using EXAMEN.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTest1
{
    public class CarServiceTests
    {
        private CarDalershipDB CreateContext()
        {
            var options = new DbContextOptionsBuilder<CarDalershipDB>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CarDalershipDB(options);
        }

        [Fact]
        public async Task GetCarsPagedAsync_ShouldFilterByBrandAndPrice()
        {
            // Arrange
            using var context = CreateContext();

            var brand1 = new Brand
            {
                Id = 1,
                Name = "BMW"
            };

            var brand2 = new Brand
            {
                Id = 2,
                Name = "Audi"
            };

            context.Brands.AddRange(brand1, brand2);

            context.Cars.AddRange(
                new Car
                {
                    Id = 1,
                    BrandId = 1,
                    Brand = brand1,
                    Price = 30000,
                    IsAvailable = true
                },
                new Car
                {
                    Id = 2,
                    BrandId = 1,
                    Brand = brand1,
                    Price = 50000,
                    IsAvailable = true
                },
                new Car
                {
                    Id = 3,
                    BrandId = 2,
                    Brand = brand2,
                    Price = 35000,
                    IsAvailable = true
                }
            );

            await context.SaveChangesAsync();

            var service = new CarService(context);

            var filter = new CarFilter
            {
                BrandId = 1,
                MinPrice = 25000,
                MaxPrice = 40000,
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await service.GetCarsPagedAsync(filter);

            // Assert
            Assert.Single(result.Cars);
            Assert.Equal(1, result.Cars[0].Id);
            Assert.Equal(1, result.Cars[0].BrandId);
            Assert.Equal(30000, result.Cars[0].Price);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task CreateCarAsync_ShouldSaveNewCar()
        {
            // Arrange
            using var context = CreateContext();

            var service = new CarService(context);

            var request = new CreateCarDtoRequest
            {
                BrandId = 1,
                Price = 30000,
                IsAvailable = true
            };

            // Act
            var result = await service.CreateCarAsync(request);

            // Assert
            Assert.NotNull(result);

            var savedCar = await context.Cars
                .FirstOrDefaultAsync(c => c.Id == result.Id);

            Assert.NotNull(savedCar);
            Assert.Equal(1, savedCar.BrandId);
            Assert.Equal(30000, savedCar.Price);
            Assert.True(savedCar.IsAvailable);
        }
    }
}