using Application;

namespace API.Core.Exceptions
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public Guid Log(Exception ex)
        {
            var id = Guid.NewGuid();
            Console.WriteLine(ex.Message + " ID: " + id);

            return id;
        }
    }
}
