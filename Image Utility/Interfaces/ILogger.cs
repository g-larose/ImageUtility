using System;
using System.Threading.Tasks;

namespace Image_Utility.Interfaces
{
    public interface ILogger
    {
        void LogInfo(DateTime timeStamp,string message);
        void LogError(DateTime timeStamp,string message);
        void LogWarning(DateTime timeStamp,string message);
        void LogDebug(DateTime timeStamp,string message);
    }
}
