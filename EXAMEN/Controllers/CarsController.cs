using EXAMEN.Domain.Models;
using EXAMEN.Domain.Plaginations.EXAMEN.Services;
using EXAMEN.DTOs.Requests;
using EXAMEN.Services.Interfaces;
using EXAMEN.Services;
using Microsoft.AspNetCore.Mvc;

namespace EXAMEN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery] CarFilter filter)
        {
            var result = await _carService.GetCarsPagedAsync(filter);

            return Ok(new
            {
                cars = result.Cars,
                totalCount = result.TotalCount,
                pageNumber = filter.PageNumber,
                pageSize = filter.PageSize
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            try
            {
                var car = await _carService.GetCarByIdAsync(id);
                return Ok(car);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(
            [FromBody] CreateCarDtoRequest carRequest)
        {
            try
            {
                var car = await _carService.CreateCarAsync(carRequest);

                return CreatedAtAction(
                    nameof(GetCarById),
                    new { id = car.Id },
                    car);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carService.DeleteCarAsync(id);

                return NoContent();
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