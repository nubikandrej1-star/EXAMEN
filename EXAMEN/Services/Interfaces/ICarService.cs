using EXAMEN.Domain.Models;
using EXAMEN.Domain.Plaginations.EXAMEN.Services;
using EXAMEN.DTOs.Requests;
using Microsoft.EntityFrameworkCore;

namespace EXAMEN.Services.Interfaces
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task<Car> CreateCarAsync(CreateCarDtoRequest carRequest);
        Task<(List<Car> Cars, int TotalCount)> GetCarsPagedAsync(CarFilter filter);
        Task DeleteCarAsync(int id);
    }
}
