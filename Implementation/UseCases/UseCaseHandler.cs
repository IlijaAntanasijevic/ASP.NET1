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
    public class UseCaseHandler
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;

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
            if (!_actor.AllowedUseCases.Contains(useCase.Id))
            {
                //   throw new UnauthorizedAccessException();
                throw new InvalidOperationException("NEAUTORIZOVANI KORISNIK!");
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
