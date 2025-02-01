using App.Domain;
using Application.DTO.Apartments;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Queries.Apartment;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;


namespace Implementation.UseCases.Queries.Apartments
{
    public class EfFindApartmentQuery : EfUseCase, IFindApartmentQuery
    {
        public EfFindApartmentQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => nameof(EfFindApartmentQuery);

        public ApartmentDto Execute(int search)
        {
            string url = new Uri($"{Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";").First()}").AbsoluteUri;
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
                                                  .FirstOrDefault(x => x.Id == search); 


            if (apartment == null)
            {
                throw new EntityNotFoundException(nameof(Apartment),search);
            }

            var apartmentDto = new ApartmentDto
            {
                Id = apartment.Id,
                City = apartment.CityCountry.City.Name,
                Name = apartment.Name,
                Description = apartment.Description,
                Country = apartment.CityCountry.Country.Name,
                MainImage = url + apartment.MainImage.Replace("wwwroot\\", ""),
                MaxGuests = apartment.MaxGuests,
                PricePerNight = apartment.Price,
                TotalBookings = apartment.Bookings.Where(x => x.ApartmentId == apartment.Id && x.IsActive).Sum(x => x.ApartmentId),
                ApartmentType = apartment.ApartmentType.Name,
                Features = apartment.FeatureApartments.Select(x => x.Feature.Name).ToList(),
                Images = apartment.Images.Select(x => url + x.Path.Replace("wwwroot\\", "")).ToList(),
                PaymentMethods = apartment.PaymentApartments.Select(x => x.Payment.Name).ToList(),
            };

            return apartmentDto;
        }
    }
}
