namespace EXAMEN.Domain.Plaginations
{
    namespace EXAMEN.Services
    {
        public class CarFilter
        {
            public int PageNumber { get; set; } = 1;
            public int PageSize { get; set; } = 10;

            public int? BrandId { get; set; }
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
            public string? FuelType { get; set; }
            public bool OnlyAvailable { get; set; }

            public CarSortOption? Sort { get; set; }
        }

        public enum CarSortOption
        {
            PriceAsc,
            PriceDesc,
            YearDesc,
            YearAsc,
            MileageAsc
        }
    }
}
