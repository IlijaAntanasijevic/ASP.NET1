using Application;
using Application.UseCases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Logging.UseCases
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(UseCaseLog log)
        {
            DateTime date = DateTime.UtcNow;
            string email = log.Email;
            string useCaseName = log.UseCaseName;
            string useCaseData = JsonConvert.SerializeObject(log.UseCaseData);

            Console.WriteLine($"Date: {date.ToLongDateString()} {date.ToLongTimeString()}, " +
                              $"Email: {email}, UseCase: {useCaseName}, Data: {useCaseData}");

        }
    }
}
