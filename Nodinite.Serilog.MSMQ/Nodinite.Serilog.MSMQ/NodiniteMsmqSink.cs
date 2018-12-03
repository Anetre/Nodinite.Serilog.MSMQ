using System;
using System.IO;
using System.Messaging;
using System.Text;
using Newtonsoft.Json;
using Nodinite.Serilog.Models;
using Nodinite.Serilog.MSMQ.Helpers;
using Serilog.Core;
using Serilog.Events;

namespace Nodinite.Serilog.MSMQ
{
    public class NodiniteMsmqSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly NodiniteMsmqSettings _msmqSettings;
        private readonly NodiniteLogEventSettings _settings;

        public NodiniteMsmqSink(NodiniteMsmqSettings msmqSettings, NodiniteLogEventSettings settings, IFormatProvider formatProvider)
        {
            _msmqSettings = msmqSettings;
            _settings = settings;
            _formatProvider = formatProvider;

            // validate settings
            if (!_settings.LogAgentValueId.HasValue)
                throw new ArgumentNullException("LogAgentValueId must not be null");
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);

            var nEvent = new NodiniteLogEvent(message, logEvent, _settings);

            LogMessage(nEvent);
        }

        public void LogMessage(NodiniteLogEvent logEvent)
        {
            MessageQueue messageQueue = new MessageQueue(_msmqSettings.QueueName);
            messageQueue.Label = "Nodinite Queue";
            
            messageQueue.Formatter = new JsonMessageFormatter(Encoding.UTF8);

            messageQueue.Send(JsonConvert.SerializeObject(logEvent), "Nodinite Log Event");
        }
    }
}