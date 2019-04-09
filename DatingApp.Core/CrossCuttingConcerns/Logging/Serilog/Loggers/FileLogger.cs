using DatingApp.Core.CrossCuttingConcerns.Logging.Serilog.Sinks;
using Serilog;

namespace DatingApp.Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class FileLogger : LoggerService
    {
        public FileLogger() : base(SerilogSinks.WriteToFile())
        {
        }
    }
}