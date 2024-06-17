using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public static class UseCaseInfo
    {
        public static IEnumerable<int> AllUseCases
        {
            get
            {
                Type type = typeof(IUseCase);
                var types = typeof(UseCaseInfo).Assembly.GetTypes()
                    .Where(type.IsAssignableFrom).Select(Activator.CreateInstance);

                return null;
            }
        }

        public static int MaxUseCaseId => 20;
    }
}
