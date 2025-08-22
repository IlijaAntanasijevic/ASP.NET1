using App.Domain;
using Application;
using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Ratings;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;


namespace Implementation.UseCases.Queries.Apartments
{
    public class EfFindApartmentQuery : EfUseCase, IFindApartmentQuery
    {
        private readonly IApplicationActor _actor;
        public EfFindApartmentQuery(BookingContext context, IApplicationActor actor) 
            : base(context)
        {
            _actor = actor;
        }

        public int Id => 16;

        public string Name => nameof(EfFindApartmentQuery);

        public ApartmentDto Execute(int search)
        {
            var apartment = Context.Apartments.Include(x => x.CityCountry)
                                                  .ThenInclude(cc => cc.City)
                                                  .Include(x => x.CityCountry)
                                                  .ThenInclude(cc => cc.Country)
                                                  .Include(x => x.User)
                                                  .Include(x => x.ApartmentType)
                                                  .Include(x => x.FeatureApartments)
                                                  .ThenInclude(f => f.Feature)
                                                  .Include(x => x.Images)
                                                  .Include(x => x.PaymentApartments)
                                                  .ThenInclude(p => p.Payment)
                                                  .Include(x => x.Bookings)
                                                  .Include(x => x.Favorites)
                                                  .Include(x => x.Ratings)
                                                  .ThenInclude(x => x.ApartmentRatings)
                                                  .FirstOrDefault(x => x.Id == search && 
                                                                 (!x.IsArchived.Value || (x.IsArchived.Value && x.UserId == _actor.Id)));


            if (apartment == null)
            {
                throw new EntityNotFoundException(nameof(Apartment), search);
            }

            var apartmentDto = new ApartmentDto
            {
                Id = apartment.Id,
                UserCanBook = apartment.UserId != _actor.Id && _actor.Id != 0,
                CanLeaveFeedback = apartment.Bookings.Any(b => b.UserId == _actor.Id && b.CheckOut < DateTime.UtcNow),
                IsFavorite = apartment.Favorites.Any(f => f.UserId == _actor.Id),
                Address = apartment.Address,
                City = new BasicDto
                {
                    Id = apartment.CityCountry.City.Id,
                    Name = apartment.CityCountry.City.Name
                },
                ApartmentTypeId = apartment.ApartmentTypeId,
                Name = apartment.Name,
                Description = apartment.Description,
                Country = new BasicDto
                {
                    Id = apartment.CityCountry.Country.Id,
                    Name = apartment.CityCountry.Country.Name
                },
                MainImage = new ApartmentImageDto
                {
                    FileName = apartment.MainImage,
                    ImageType = UploadType.MainImage,
                    OriginalFileName = null
                },
                MaxGuests = apartment.MaxGuests,
                PricePerNight = apartment.Price,
                TotalBookings = apartment.Bookings.Where(x => x.IsActive).Count(),
                ApartmentType = apartment.ApartmentType.Name,
                Features = apartment.FeatureApartments.Select(x => new BasicDto
                {
                    Id = x.Feature.Id,
                    Name = x.Feature.Name
                }),
                Images = apartment.Images.Select(x => new ApartmentImageDto
                {
                    FileName = x.Path,
                    ImageType = UploadType.Apartment,
                    OriginalFileName = null
                }).ToList(),
                PaymentMethods = apartment.PaymentApartments.Select(x => new BasicDto
                {
                    Id = x.Payment.Id,
                    Name = x.Payment.Name
                }),
                Longitude = apartment.Longitude ?? 16.363449m,
                Lattitude = apartment.Lattitude ?? 48.210033m,
                RatingInfo = new RatingDto
                {
                    TotalRatings = apartment.Ratings.Count(),
                    AvgRating = apartment.Ratings.Any() ? apartment.Ratings.Average(a => a.ApartmentRatings.Average(x => (float)x.StarRating)) : 0,
                    RatingStatistic = apartment.Ratings.SelectMany(x => x.ApartmentRatings).GroupBy(g => new { g.RatingTypeId }).Select(g => new RatingValuesDto
                    {
                        Id = g.Key.RatingTypeId,
                        Value = (float)Math.Round(g.Average(ar => ar.StarRating), 1)
                    }).ToList()
                }
              
            };

            return apartmentDto;
        }
    }
}
