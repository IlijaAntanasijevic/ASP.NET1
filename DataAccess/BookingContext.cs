using Domain;
using Domain.Lookup;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BookingContext : DbContext
    {
        private readonly string _connectionString;

        public BookingContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BookingContext()
        {
            _connectionString = "Data Source=DESKTOP-VONR1CS\\SQLEXPRESS;Initial Catalog=Booking2;Integrated Security=True;Trust Server Certificate=True";

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Payment> Payments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CityCountry> CitiesCountry { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<FeatureApartment> FeatureApartments { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
