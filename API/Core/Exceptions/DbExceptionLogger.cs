using Application;

namespace API.Core.Exceptions
{
    public class DbExceptionLogger : IExceptionLogger
    {
        public Guid Log(Exception ex, IApplicationActor actor)
        {
            throw new NotImplementedException();
        }
    }
}
