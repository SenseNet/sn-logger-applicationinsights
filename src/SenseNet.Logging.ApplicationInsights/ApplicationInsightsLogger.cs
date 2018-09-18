using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SenseNet.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace SenseNet.Logging.ApplicationInsights
{
    [SuppressMessage("ReSharper", "ConvertToConstant.Local")]
    public class ApplicationInsightsLogger : IEventLogger
    {
        private static readonly string DefaultEventName = "sensenet";
        private static readonly int MaxPropertyNameLength = 512;
        private static readonly int MaxValueLength = 8192;

        private readonly TelemetryClient _telemetry = new TelemetryClient();

        public ApplicationInsightsLogger()
        {
            _telemetry.Context.Device.Id = Guid.NewGuid().ToString();
        }

        public void Write(object message, ICollection<string> categories, int priority, int eventId, 
            TraceEventType severity, string title, IDictionary<string, object> properties)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (severity)
            {
                case TraceEventType.Critical:
                case TraceEventType.Error:
                    var exceptionTelemetry = new ExceptionTelemetry();
                    AddProperties(exceptionTelemetry, message, categories, eventId, title, properties);
                    _telemetry.TrackException(exceptionTelemetry);
                    break;
                default:
                    var eventTelemetry = new EventTelemetry(DefaultEventName);
                    AddProperties(eventTelemetry, message, categories, eventId, title, properties);
                    _telemetry.TrackEvent(eventTelemetry);
                        break;
            }
        }

        private static void AddProperties(ISupportProperties telemetry, object message, ICollection<string> categories, 
            int eventId, string title, IDictionary<string, object> properties)
        {
            telemetry.Properties.Add("Message", message?.ToString().Substring(0, MaxValueLength));
            telemetry.Properties.Add("EventId", eventId.ToString());

            if (categories != null && categories.Count > 0)
                telemetry.Properties.Add("Categories", string.Join(", ", categories));

            if (!string.IsNullOrEmpty(title))
                telemetry.Properties.Add("Title", title);

            if (properties?.Any() ?? false)
                foreach (var property in properties.Where(p => !string.IsNullOrEmpty(p.Key)))
                    telemetry.Properties.Add(property.Key.Substring(MaxPropertyNameLength), property.Value?.ToString().Substring(0, MaxValueLength));
        }
    }
}
