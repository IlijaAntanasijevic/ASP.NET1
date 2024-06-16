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
                var type = typeof(IUseCase);
                var types = typeof(UseCaseInfo).Assembly.GetTypes()
                    .Where(p => type.IsAssignableFrom(p)).Select(x => Activator.CreateInstance(x));

                return null;
            }
        }

        public static int MaxUseCaseId => 13;
    }
}
