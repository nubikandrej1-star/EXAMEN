using EXAMEN.Data.DBContext;
using EXAMEN.Domain.Models;
using EXAMEN.Domain.Plaginations.EXAMEN.Services;
using EXAMEN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EXAMEN.Domain.Validators;
using EXAMEN.Domain.Plaginations;
using EXAMEN.DTOs.Requests;

namespace EXAMEN.Services
{
    public class CarService : ICarService
    {
        private readonly CarDalershipDB _context;
        private readonly CreateCarDtoValidator createCarDtoValidator = new CreateCarDtoValidator();

        public CarService(CarDalershipDB context)
        {
            _context = context;
        }

        public async Task<(List<Car> Cars, int TotalCount)> GetCarsPagedAsync(CarFilter filter)
        {
            if (filter.PageNumber < 1)
                filter.PageNumber = 1;

            if (filter.PageSize < 1)
                filter.PageSize = 10;

            IQueryable<Car> query = _context.Cars
                .Include(c => c.Brand)
                .AsNoTracking();

            if (filter.BrandId.HasValue)
            {
                query = query.Where(c => c.BrandId == filter.BrandId.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(c => c.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(c => c.Price <= filter.MaxPrice.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.FuelType))
            {
                query = query.Where(c => c.FuelType == filter.FuelType);
            }

            if (filter.OnlyAvailable)
            {
                query = query.Where(c => c.IsAvailable);
            }

            query = filter.Sort switch
            {
                CarSortOption.PriceAsc => query.OrderBy(c => c.Price),

                CarSortOption.PriceDesc => query.OrderByDescending(c => c.Price),

                CarSortOption.YearDesc => query.OrderByDescending(c => c.Year),

                CarSortOption.YearAsc => query.OrderBy(c => c.Year),

                CarSortOption.MileageAsc => query.OrderBy(c => c.Mileage),
                _ => query.OrderBy(c => c.Id)
            };

            int totalCount = await query.CountAsync();

            List<Car> cars = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (cars, totalCount);
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _context.Cars
                .Include(c => c.Brand)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Car> GetCarByIdAsync(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }
            return car;
        }

        public async Task<Car> CreateCarAsync(CreateCarDtoRequest carRequest)
        {
            Car car = new Car();
            car.UpdateCar(carRequest);
            var validationResult = createCarDtoValidator.Validate(carRequest);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Invalid car data" + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateCarAsync(int id, CreateCarDtoRequest carRequest)
        {
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null)
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }

            var validationResult = createCarDtoValidator.Validate(carRequest);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Invalid car data" + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            existingCar.UpdateCar(carRequest);
            await _context.SaveChangesAsync();
            return existingCar;
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}