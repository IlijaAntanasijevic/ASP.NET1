using Application.DTO;
using Application.DTO.Search;
using Application.UseCases;
using DataAccess;
using Domain.Core;
using Implementation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public class EfUseCase
    {
        private readonly BookingContext _context;

        protected EfUseCase(BookingContext context)
        {
            _context = context;
        }

        protected BookingContext Context => _context;
    }

    public abstract class EfDeleteCommand<TEntity> : EfUseCase, ICommand<int>
        where TEntity : Entity
    {
        protected EfDeleteCommand(BookingContext context) : base(context)
        {
        }

        public abstract int Id { get; }
        public abstract string Name { get; }

        public virtual void Execute(int id)
        {
            var item = Context.Set<TEntity>().Find(id);

            if(item == null || !item.IsActive)
            {
                throw new EntityNotFoundException(nameof(TEntity), id);
            }

            item.IsActive = false;
            Context.SaveChanges();

        }
    }
}
