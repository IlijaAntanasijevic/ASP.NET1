using Application;
using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Logging.UseCases
{
    public class DbUseCaseLogger : IUseCaseLogger
    {
        private readonly BookingContext _context;

        public DbUseCaseLogger(BookingContext context)
        {
            _context = context;
        }

        public void Log(UseCaseLog log)
        {
            var dbLog = new App.Domain.UseCaseLog
            {
                Email = log.Email,
                UseCaseData = JsonConvert.SerializeObject(log.UseCaseData),
                UseCaseName = log.UseCaseName,
                ExecutedAt = DateTime.UtcNow
            };

            _context.UseCaseLogs.Add(dbLog);
            _context.SaveChanges();
        }
    }
}
