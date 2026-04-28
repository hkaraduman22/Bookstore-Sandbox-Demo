using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Services.Conctrats
{
    public interface ILoggingService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogDebug(string message);


    }
}
