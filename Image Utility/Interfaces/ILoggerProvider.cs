using Microsoft.Extensions.Logging;

namespace Image_Utility.Interfaces
{
    public interface ILoggerProvider
    {
        public ILogger CreateLogger();
    }
}
