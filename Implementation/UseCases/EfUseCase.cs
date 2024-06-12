using DataAccess;
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
}
