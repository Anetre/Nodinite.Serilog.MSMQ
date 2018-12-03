using System;
using System.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nodinite.Serilog.Models;
using Nodinite.Serilog.MSMQ;
using Serilog;
using Serilog.Core;

namespace Nodinite.Serilog.Msmq.TEsts
{
    [TestClass]
    public class NodiniteMsmqSinkTests
    {
        [TestMethod]
        public void SendMessageToMsmqTest()
        {
            var settings = new NodiniteLogEventSettings()
            {
                LogAgentValueId = 503,
                EndPointDirection = 0,
                EndPointTypeId = 0,
                EndPointUri = "Nodinite.Serilog.ApiSink.Tests.Serilog",
                EndPointName = "Nodinite.Serilog.ApiSink.Tests",
                OriginalMessageTypeName = "Serilog.LogEvent",
                ProcessingUser = "NODINITE",
                ProcessName = "Nodinite.Serilog.ApiSink.Tests",
                ProcessingMachineName = "NODINITE-DEV",
                ProcessingModuleName = "DOTNETCORE.TESTS",
                ProcessingModuleType = "DOTNETCORE.TESTPROJECT"
            };

            Logger log = new LoggerConfiguration()
    .WriteTo.NodiniteMsmqSink(new NodiniteMsmqSettings() { QueueName = @".\private$\nodiniteserilog" }, settings)
    .CreateLogger();

            log.Information("Hello World");
        }
    }
}
