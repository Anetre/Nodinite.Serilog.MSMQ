using Nodinite.Serilog.Models;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodinite.Serilog.MSMQ
{
    public static class NodiniteMsmqSinkExtensions
    {
        public static LoggerConfiguration NodiniteMsmqSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  NodiniteMsmqSettings MsmqSettings,
                  NodiniteLogEventSettings Settings,
                  IFormatProvider formatProvider = null,
                  LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException("loggerConfiguration");

            return loggerConfiguration.Sink(new NodiniteMsmqSink(MsmqSettings, Settings, formatProvider), restrictedToMinimumLevel);
        }
    }
}
