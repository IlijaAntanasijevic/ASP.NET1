using Application;
using Application.UseCases;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Implementation.UseCases
{
    public class UseCaseHandler
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;
        //2 - Register | 6 - Find User | 4 - Get All Users | 4 - Get All Apartment Types
        //15 - Get All Apartments | 16 - Find Apartment | 25+ Lookup tables get all | 42 - Get Apartment Ratings

        //PRIVREMENO - 34,35,36,38,37(MyGuestBookings),39 (AddToFavorite), 40 (Get Favorites)
        //41 - Create rating, 42 - Get Rating, 43 - Archive Apartment, 44 - Get Archived Apartment, 45 - Activate Apartment
        //46 - Confirm email, 47 - Resend email, 48 - Forgot Pass Send Email, 49 - Check Code, 50 - Change Password
        private List<int> GloballyAllowed = new List<int> { 2, 3, 6, 4, 15, 16, 25,26,27,28,29,32,42, /*start(delete)*/34,35,36,38,37,39,40,41,42,43,44,45,46,47,48,49,50 /*end*/};

        public UseCaseHandler(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public void HandleCommand<TData>(ICommand<TData> command, TData data)
        {
            Handle(command, data);

            command.Execute(data);
        }

        public TResult HandleQuery<TResult, TSearch>(IQuery<TResult, TSearch> query, TSearch search)
            where TResult : class
        {
            Handle(query, search);

            return query.Execute(search);
        }


        private void Handle(IUseCase useCase, object data)
        {
            if (!GloballyAllowed.Contains(useCase.Id) && !_actor.AllowedUseCases.Contains(useCase.Id))
            {
                throw new UnauthorizedAccessException();
            }
            var log = new UseCaseLog
            {
                UseCaseData = data,
                UseCaseName = useCase.Name,
                Email = _actor.Email,
            };
            _logger.Log(log);
        }
    }
}
