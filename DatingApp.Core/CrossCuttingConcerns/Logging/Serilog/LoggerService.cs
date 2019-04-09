using System;
using Serilog;
using Serilog.Events;

namespace DatingApp.Core.CrossCuttingConcerns.Logging.Serilog
{
    [Serializable]
    public class LoggerService
    {
        public readonly bool isDebug;
        public readonly bool isError;
        public readonly bool isInformation;
        public readonly bool isFattal;
        public readonly bool isWarning;
        private readonly ILogger log;
        
        public LoggerService(ILogger log)
        {
            this.log = log;

            isDebug=log.IsEnabled(LogEventLevel.Debug);
            isError=log.IsEnabled(LogEventLevel.Error);
            isInformation=log.IsEnabled(LogEventLevel.Information);
            isFattal=log.IsEnabled(LogEventLevel.Fatal);
            isWarning=log.IsEnabled(LogEventLevel.Warning);
         
        }


        public void Debug(object LogMassage)
        {
            if(isDebug)
            {
                log.Debug("Processing Debug Message:{@LogMessage}",LogMassage);
            }
        }
        public void Error(object LogMassage)
        {
            if(isError)
            {
                log.Error("Processing Error Message:{@LogMessage}",LogMassage);
            }
        }
        public void Information(object LogMassage)
        {
            if(isInformation)
            {
                log.Information("Processing Information Message:{@LogMessage}",LogMassage);
            }
        }
        public void Fattal(object LogMassage)
        {
            if(isFattal)
            {
                log.Fatal("Processing Fattal Message:{@LogMessage}",LogMassage);
            }
        }
        public void warning(object LogMassage)
        {
            if(isWarning)
            {
                log.Warning("Processing Warning Message:{@LogMessage}",LogMassage);
            }
        }


       
    }
}