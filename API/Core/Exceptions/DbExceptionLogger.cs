using Application;
using DataAccess;
using Domain;

namespace API.Core.Exceptions
{
    public class DbExceptionLogger : IExceptionLogger
    {
        private readonly BookingContext _context;

        public DbExceptionLogger(BookingContext context)
        {
            _context = context;
        }

        public Guid Log(Exception ex)
        {
            Guid id = Guid.NewGuid();

            ErrorLog log = new ErrorLog
            {
                ErrorId = id,
                Message = ex.Message,
                StrackTrace = ex.StackTrace,
                Time = DateTime.Now,
            };
            _context.ErrorLogs.Add(log);
            _context.SaveChanges();

            return id;
        }
    }
}
