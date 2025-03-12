//using System;
//using Azure.Messaging.EventHubs;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Extensions.Logging;

//namespace POC_MangoDB_EventHub_trigger
//{
//    public class EventHubRec
//    {
//        private readonly ILogger<EventHubRec> _logger;

//        public EventHubRec(ILogger<EventHubRec> logger)
//        {
//            _logger = logger;
//        }

//        [Function("EventHubRec")]
//        public void Run([EventHubTrigger("eventhub-01", Connection = "Endpoint=sb://poceventhub27.servicebus.windows.net/;SharedAccessKeyName=SendListenPolicy;SharedAccessKey=//7lmEZJQ/3HmC2f+OG83fota3HQUxpqZ+AEhL+oJdA=")] EventData[] events)
//        {
//            foreach (EventData @event in events)
//            {
//                _logger.LogInformation("Event Body: {body}", @event.Body);
//                _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
//            }
//        }
//    }
//}
