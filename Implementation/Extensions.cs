using App.Domain;
using Application.Exceptions;
using DataAccess;
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
       
    }
}
