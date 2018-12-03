using System;
using Nodinite.Serilog.Models;
using Serilog.Core;
using Serilog.Events;

namespace Nodinite.Serilog.MSMQ
{
    public class NodiniteMsmqSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly string _apiUrl;
        private readonly NodiniteLogEventSettings _settings;

        public NodiniteMsmqSink(string apiUrl, NodiniteLogEventSettings settings, IFormatProvider formatProvider)
        {
            _apiUrl = apiUrl;
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
            // post message to msmq
        }
    }
}