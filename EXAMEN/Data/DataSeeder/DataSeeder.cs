using EXAMEN.Domain.Models;
using EXAMEN.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace EXAMEN.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(CarDalershipDB context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Brands.AnyAsync())
            {
                var brands = new List<Brand>
                {
                    new Brand
                    {
                        Name = "Toyota",
                        Country = "Japan"
                    },
                    new Brand
                    {
                        Name = "BMW",
                        Country = "Germany"
                    },
                    new Brand
                    {
                        Name = "Mercedes-Benz",
                        Country = "Germany"
                    },
                    new Brand
                    {
                        Name = "Ford",
                        Country = "USA"
                    },
                    new Brand
                    {
                        Name = "Tesla",
                        Country = "USA"
                    }
                };

                await context.Brands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
            }

            if (!await context.Cars.AnyAsync())
            {
                var toyota = await context.Brands.FirstAsync(b => b.Name == "Toyota");
                var bmw = await context.Brands.FirstAsync(b => b.Name == "BMW");
                var mercedes = await context.Brands.FirstAsync(b => b.Name == "Mercedes-Benz");
                var ford = await context.Brands.FirstAsync(b => b.Name == "Ford");
                var tesla = await context.Brands.FirstAsync(b => b.Name == "Tesla");

                var cars = new List<Car>
                {
                    new Car
                    {
                        BrandId = toyota.Id,
                        Model = "Camry",
                        Year = 2022,
                        Price = 28000,
                        Mileage = 35000,
                        FuelType = "Petrol",
                        Transmission = "Automatic",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = toyota.Id,
                        Model = "Corolla",
                        Year = 2021,
                        Price = 22000,
                        Mileage = 42000,
                        FuelType = "Petrol",
                        Transmission = "Automatic",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = bmw.Id,
                        Model = "3 Series",
                        Year = 2023,
                        Price = 45000,
                        Mileage = 18000,
                        FuelType = "Petrol",
                        Transmission = "Automatic",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = bmw.Id,
                        Model = "X5",
                        Year = 2022,
                        Price = 62000,
                        Mileage = 27000,
                        FuelType = "Diesel",
                        Transmission = "Automatic",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = mercedes.Id,
                        Model = "C-Class",
                        Year = 2023,
                        Price = 51000,
                        Mileage = 15000,
                        FuelType = "Petrol",
                        Transmission = "Automatic",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = mercedes.Id,
                        Model = "GLE",
                        Year = 2021,
                        Price = 58000,
                        Mileage = 40000,
                        FuelType = "Diesel",
                        Transmission = "Automatic",
                        IsAvailable = false
                    },
                    new Car
                    {
                        BrandId = ford.Id,
                        Model = "Mustang",
                        Year = 2020,
                        Price = 38000,
                        Mileage = 30000,
                        FuelType = "Petrol",
                        Transmission = "Manual",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = ford.Id,
                        Model = "Focus",
                        Year = 2019,
                        Price = 18000,
                        Mileage = 55000,
                        FuelType = "Petrol",
                        Transmission = "Manual",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = tesla.Id,
                        Model = "Model 3",
                        Year = 2023,
                        Price = 42000,
                        Mileage = 12000,
                        FuelType = "Electric",
                        Transmission = "Automatic",
                        IsAvailable = true
                    },
                    new Car
                    {
                        BrandId = tesla.Id,
                        Model = "Model Y",
                        Year = 2022,
                        Price = 48000,
                        Mileage = 20000,
                        FuelType = "Electric",
                        Transmission = "Automatic",
                        IsAvailable = true
                    }
                };

                await context.Cars.AddRangeAsync(cars);
                await context.SaveChangesAsync();
            }

            if (!await context.BookingRequests.AnyAsync())
            {
                var cars = await context.Cars
                    .Where(c => c.IsAvailable)
                    .Take(3)
                    .ToListAsync();

                if (cars.Count >= 3)
                {
                    var bookings = new List<BookingRequest>
                    {
                        new BookingRequest
                        {
                            CarId = cars[0].Id,
                            CustomerName = "Andriy Petrenko",
                            CustomerPhone = "+380501234567",
                            CustomerEmail = "andriy@example.com",
                            PreferredDate = DateTime.Now.AddDays(3),
                            Status = "Pending"
                        },
                        new BookingRequest
                        {
                            CarId = cars[1].Id,
                            CustomerName = "Ivan Kovalenko",
                            CustomerPhone = "+380671234567",
                            CustomerEmail = "ivan@example.com",
                            PreferredDate = DateTime.Now.AddDays(5),
                            Status = "Approved"
                        },
                        new BookingRequest
                        {
                            CarId = cars[2].Id,
                            CustomerName = "Olena Shevchenko",
                            CustomerPhone = "+380931234567",
                            CustomerEmail = "olena@example.com",
                            PreferredDate = DateTime.Now.AddDays(7),
                            Status = "Pending"
                        }
                    };

                    await context.BookingRequests.AddRangeAsync(bookings);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}