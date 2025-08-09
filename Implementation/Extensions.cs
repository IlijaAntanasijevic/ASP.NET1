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

namespace Implementation
{
    public static class Extensions
    {
        public static bool ApartmentIsAvailable(this IEnumerable<Booking> bookings, CheckApartmentDto data)
        {
            if (data.CheckIn == null || data.CheckOut == null) return true;

            return !bookings.Any(x => x.ApartmentId == data.ApartmentId && x.IsActive &&
                                                        ((data.CheckIn >= x.CheckIn && data.CheckIn < x.CheckOut) ||
                                                        (data.CheckOut > x.CheckIn && data.CheckOut <= x.CheckOut) ||
                                                        (data.CheckIn < x.CheckIn && data.CheckOut > x.CheckOut)));
        }

        public static Expression<Func<Apartment, bool>> ApartmentIsAvailable(DateTime checkIn, DateTime checkOut)
        {
            return a => !a.Bookings.Any(b => b.IsActive &&
                ((checkIn >= b.CheckIn && checkIn < b.CheckOut) ||
                 (checkOut > b.CheckIn && checkOut <= b.CheckOut) ||
                 (checkIn < b.CheckIn && checkOut > b.CheckOut)));
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
    }


}
