using DatingApp.Core.CrossCuttingConcerns.Logging.Serilog.Sinks;

namespace DatingApp.Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class PostgreSqlLogger:LoggerService
    {
         public PostgreSqlLogger() : base(SerilogSinks.WriteToPostgreSql())
        {
            
        }
    }
}