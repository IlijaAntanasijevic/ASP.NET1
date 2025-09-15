using App.Domain;
using Application.DTO;
using Application.DTO.Apartments;
using Application.DTO.Search;
using Application.Exceptions;
using DataAccess;
using Domain.Lookup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Implementation.Common
{
    public static class Extensions
    {
        public static bool ApartmentIsAvailable(this IEnumerable<Booking> bookings, CheckApartmentDto data)
        {
            if (data.CheckIn == null || data.CheckOut == null) return true;

            return !bookings.Any(x => x.ApartmentId == data.ApartmentId && x.IsActive &&
                                                        (data.CheckIn >= x.CheckIn && data.CheckIn < x.CheckOut ||
                                                        data.CheckOut > x.CheckIn && data.CheckOut <= x.CheckOut ||
                                                        data.CheckIn < x.CheckIn && data.CheckOut > x.CheckOut));
        }

        public static Expression<Func<Apartment, bool>> ApartmentIsAvailable(DateTime checkIn, DateTime checkOut)
        {
            return a => !a.Bookings.Any(b => b.IsActive &&
                (checkIn >= b.CheckIn && checkIn < b.CheckOut ||
                 checkOut > b.CheckIn && checkOut <= b.CheckOut ||
                 checkIn < b.CheckIn && checkOut > b.CheckOut));
        }



        public static void DoesNotExist<TEntity>(this BookingContext context, Expression<Func<TEntity, bool>> method)
            where TEntity : class
        {
            var exists = context.Set<TEntity>().Any(method);
            var tmp = method.GetType().Name;
            if (exists)
            {
                throw new ConflictException($"This {tmp} already exists");
            }
        }


        public static IQueryable<BasicDto> ApplySearch<TQuery>(this IQueryable<TQuery> query, Func<TQuery, string> nameSelector, BasicSearch search) 
            where TQuery : class
        {
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => nameSelector(x).ToLower().Contains(search.Keyword.ToLower()));
            }

            return query.Select(x => new BasicDto
            {
                Id = (int)x.GetType().GetProperty("Id").GetValue(x),
                Name = nameSelector(x)
            });
        }

        public static string LoadTemplateHtml(string fileName, Dictionary<string, string> placeholders)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Templates", fileName);
            var html = File.ReadAllText(path);

            foreach(var item in placeholders)
            {
                html = html.Replace($"{{{item.Key}}}", item.Value);
            }

            return html;
        }

        public static List<BasicDto> GetTotalBookingsFilter()
        {
            return new List<BasicDto>
            {
                new BasicDto
                {
                     Id = 1,
                     Name = "5+"
                },
                new BasicDto
                {
                     Id = 2,
                     Name = "10+"
                },
                new BasicDto
                {
                     Id = 3,
                     Name = "25+"
                },
                new BasicDto
                {
                     Id = 4,
                     Name = "50+"
                }
            };
        }

        public static List<UserUseCase> AddUserUseCases()
        {
            //7 - Delete user
            //8 - Update user
            //9 - Create Apartment
            //17 - Delete Apartment
            //18 - Update Apartment
            //19 - Update Apartment Images
            //20 - Create Booking
            //21 - Update Booking
            //22 - Delete Booking
            //23 - Get Bookings
            //24 - Find Booking
            //33 - Change profile photo
            //34 - Send message
            //35 - User Chat List
            //36 - Chat Messages
            //37 - My Guest Bookings
            //38 - Prepare Chat
            //39 - Add Apartment To Favorite
            //40 - Get Favorite Apartments
            //41 - Create Rating
            //43 - Archive Apartment
            //44 - Get Archived Apartments
            //45 - Activate Apartment
            //60 - Get Cities With Details

            return new List<UserUseCase>()
            {
                //new UserUseCase { UseCaseId = 7},
                new UserUseCase { UseCaseId = 8},
                new UserUseCase { UseCaseId = 9},
                new UserUseCase { UseCaseId = 17},
                new UserUseCase { UseCaseId = 18},
                new UserUseCase { UseCaseId = 19},
                new UserUseCase { UseCaseId = 20},
                new UserUseCase { UseCaseId = 21},
                new UserUseCase { UseCaseId = 22},
                new UserUseCase { UseCaseId = 23},
                new UserUseCase { UseCaseId = 24},
                new UserUseCase { UseCaseId = 33},
                new UserUseCase { UseCaseId = 34},
                new UserUseCase { UseCaseId = 35},
                new UserUseCase { UseCaseId = 36},
                new UserUseCase { UseCaseId = 37},
                new UserUseCase { UseCaseId = 38},
                new UserUseCase { UseCaseId = 39},
                new UserUseCase { UseCaseId = 40},
                new UserUseCase { UseCaseId = 41},
                new UserUseCase { UseCaseId = 43},
                new UserUseCase { UseCaseId = 44},
                new UserUseCase { UseCaseId = 45},
                new UserUseCase { UseCaseId = 60},
             };
        }
    }

}
