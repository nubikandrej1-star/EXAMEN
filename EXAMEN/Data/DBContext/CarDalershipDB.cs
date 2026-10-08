using Microsoft.EntityFrameworkCore;
using EXAMEN.Domain.Models;
using FluentValidation;


namespace EXAMEN.Data.DBContext
{
    public class CarDalershipDB : DbContext
    {
        public CarDalershipDB(DbContextOptions<CarDalershipDB> options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<BookingRequest> BookingRequests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId);

            modelBuilder.Entity<BookingRequest>()
                .HasOne(br => br.Car)
                .WithMany(c => c.BookingRequests)
                .HasForeignKey(br => br.CarId);
        }
    }
}
