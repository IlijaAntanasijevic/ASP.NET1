using App.Domain;
using Application.Exceptions;
using Application.UseCases.Queries.Admin;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.Admin
{
    public class EfGetUserUseCasesQuery : EfUseCase, IGetUserUseCasesQuery
    {
        public EfGetUserUseCasesQuery(BookingContext context) : base(context)
        {
        }

        public int Id => 58;

        public string Name => nameof(EfGetUserUseCasesQuery);

        public List<int> Execute(int userId)
        {
            var user = Context.Users.Include(x => x.UseCases).FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), userId);
            }

            return  user.UseCases.Select(x => x.UseCaseId).ToList();
        }
    }
}
