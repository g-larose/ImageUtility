using System;
using System.Threading.Tasks;

namespace Image_Utility.Interfaces
{
    public interface ILogger
    {
        void Log(DateTime timeStamp,string message);
    }
}
