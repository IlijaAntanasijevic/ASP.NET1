using Application;

namespace API.Core.Exceptions
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public Guid Log(Exception ex, IApplicationActor actor)
        {
            var id = Guid.NewGuid();
            Console.WriteLine(ex.Message + " ID: " + id + " Email:" + actor.Email);

            return id;
        }
    }
}
