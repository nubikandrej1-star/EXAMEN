using EXAMEN.DTOs.Requests;
using System.Text.Json.Serialization;

namespace EXAMEN.Domain.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }

        public Brand? Brand { get; set; }
        [JsonIgnore]
        public ICollection<BookingRequest>? BookingRequests { get; set; }


        public void UpdateCar(CreateCarDtoRequest updatedCar)
        {
            BrandId = updatedCar.BrandId;
            Model = updatedCar.Model;
            Year = updatedCar.Year;
            Price = updatedCar.Price;
            Mileage = updatedCar.Mileage;
            FuelType = updatedCar.FuelType;
            Transmission = updatedCar.Transmission;
            IsAvailable = updatedCar.IsAvailable;
        }

        public CreateCarDtoRequest ToCreateCarDtoRequest()
        {
            return new CreateCarDtoRequest
            {
                BrandId = this.BrandId,
                Model = this.Model,
                Year = this.Year,
                Price = this.Price,
                Mileage = this.Mileage,
                FuelType = this.FuelType,
                Transmission = this.Transmission,
                IsAvailable = this.IsAvailable
            };
        }
    }
}
