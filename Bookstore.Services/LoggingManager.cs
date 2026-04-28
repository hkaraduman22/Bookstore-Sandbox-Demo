using Bookstore.Services.Conctrats;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Services
{
    public class LoggingManager:ILoggingService
    {

        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message) => logger.Debug(message);


        public void LogError(string message) => logger.Error(message);


        public void LogInfo(string message) => logger.Info(message);


        public void LogWarning(string message) => logger.Warn(message);
    }
}
