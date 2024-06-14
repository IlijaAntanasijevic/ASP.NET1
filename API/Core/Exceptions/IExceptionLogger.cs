using Application;

namespace API.Core.Exceptions
{
    public interface IExceptionLogger
    {
        Guid Log(Exception ex);
    }
}
