using Application;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Implementation.UseCases
{
    /*
    To-do:

    Booking :
    - update
    - delete = cancel
    - find
    - get all

    - Lookup tables (select *)
    */

    public class UseCaseHandler
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;
        //2 - Register | 6 - Find User | 4 - Get All Users | 4 - Get All Apartment Types
        //15 - Get All Apartments | 16 - Find Apartment 

        private List<int> GloballyAllowed = new List<int> { 2, 3, 6, 4, 15, 16 };

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
