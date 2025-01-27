using System;
using Image_Utility.Enums;

namespace Image_Utility.Interfaces
{
    public interface ILogger
    {
        void Log(DateTime timeStamp,string message, LogType type, TargetType target);
    }
}
