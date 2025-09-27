using Application.DTO;
using Application.UseCases.Queries.Home;
using DataAccess;

namespace Implementation.UseCases.Queries.Home
{
    public class EfGetHomeStatsQuery : EfUseCase, IGetHomeStatsQuery
    {
        public EfGetHomeStatsQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 65;

        public string Name => nameof(EfGetHomeStatsQuery);

        public HomeStatsDto Execute(int search)
        {
            int totalUser = Context.Users.Count();
            int totalApartments = Context.Apartments.Count();
            int totalBookings = Context.Bookings.Count();
            double avgRating = Context.Ratings.SelectMany(r => r.ApartmentRatings).Select(a => (double?)a.StarRating).Average() ?? 0.0;
            int totalReviews = Context.Ratings.Count();

            int rounderBy10 = (int)Math.Pow(10, (int)Math.Floor(Math.Log10(totalReviews)));

            int roundedReviews = rounderBy10 == 0 ? 0 : (totalReviews / rounderBy10) * rounderBy10;

            return new HomeStatsDto
            {
                TotalUsers = totalUser,
                TotalApartments = totalApartments,
                TotalBookings = totalBookings,
                AvgRating = avgRating,
                TotalReviews = roundedReviews
            };
        }
    }
}
