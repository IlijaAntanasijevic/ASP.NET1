using Domain;
using App.Domain;
using Domain.Lookup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Core;

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
            _connectionString = "Data Source=DESKTOP-VOSA27V\\SQLEXPRESS;Initial Catalog=Booking2;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);


            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId });

            modelBuilder.Entity<FavoriteApartments>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.User)
                      .WithMany(x => x.Favorites)
                      .HasForeignKey(x => x.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Apartment)
                      .WithMany(x => x.Favorites)
                      .HasForeignKey(x => x.ApartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ChatMessages>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Sender)
                      .WithMany()
                      .HasForeignKey(x => x.SenderId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Receiver)
                      .WithMany()
                      .HasForeignKey(x => x.ReceiverId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
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
        public DbSet<PaymentApartment> PaymentApartments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPayment> BookingPayments { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
        public DbSet<FavoriteApartments> FavoriteApartments { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }
    }
}
