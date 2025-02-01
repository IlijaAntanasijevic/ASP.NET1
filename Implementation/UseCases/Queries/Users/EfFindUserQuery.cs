using App.Domain;
using Application.DTO.Users;
using Application.Exceptions;
using Application.UseCases.Queries.Users;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;


namespace Implementation.UseCases.Queries.Users
{
    public class EfFindUserQuery : EfUseCase, IFindUserQuery
    {
        public EfFindUserQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => nameof(EfFindUserQuery);

        public UserDto Execute(int userId)
        {
            string url = new Uri($"{Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(";").First()}").AbsoluteUri;
            User user = Context.Users.Include(x => x.Apartments)
                                     .ThenInclude(x => x.Bookings)
                                     .FirstOrDefault(x => x.Id == userId && x.IsActive);
            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), userId);
            }

            return new UserDto
            {
                Avatar = url + "users/default.jpg",
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Id = userId,
            };
        }
    }
}
