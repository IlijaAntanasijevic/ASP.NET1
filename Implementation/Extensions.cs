using App.Domain;
using Application.DTO.Search;
using Application.DTO;
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
        public static bool ApartmentIsAvailable(this BookingContext context, DateTime checkIn, DateTime checkOut)
        {
            return context.Bookings.Any(x =>
             (checkIn >= x.CheckIn && checkIn < x.CheckOut) ||
             (checkOut > x.CheckIn && checkOut <= x.CheckOut) ||
             (checkIn < x.CheckIn && checkOut > x.CheckOut));
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
