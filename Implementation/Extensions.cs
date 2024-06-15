using Application.Exceptions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public static class Extensions
    {

        public static void EnsureDoesNotExist<TEntity>(this BookingContext context, Expression<Func<TEntity, bool>> method)
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
