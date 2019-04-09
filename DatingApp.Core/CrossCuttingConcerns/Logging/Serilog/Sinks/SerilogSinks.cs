using System.Collections.Generic;
using NpgsqlTypes;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.PostgreSQL;

namespace DatingApp.Core.CrossCuttingConcerns.Logging.Serilog.Sinks
{
    public static class SerilogSinks
    {
         public static Logger WriteToFile()
        {
            var log=new LoggerConfiguration()
                      .MinimumLevel.Information()
                      .MinimumLevel.Override("Microsoft",LogEventLevel.Information)
                      .Enrich.FromLogContext()  
                      .WriteTo.File(new CompactJsonFormatter(),"log.txt",rollingInterval:RollingInterval.Day)
                      .CreateLogger();

            return log;
        }

        public static Logger WriteToPostgreSql()
        {
            string connectionstring = "User ID=postgres;Password=466357;Host=localhost;Port=5432;Database=DatingApp";
            string tableName = "logs";
            
            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
                {
                    {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                    {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                    {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                    {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                    {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                    {"properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                    {"props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                    {"machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
                };

           var log=new LoggerConfiguration()
                       .WriteTo.PostgreSQL(connectionstring,tableName,columnWriters,needAutoCreateTable:true)
                       .CreateLogger();

            return log;  

        }
    }
}