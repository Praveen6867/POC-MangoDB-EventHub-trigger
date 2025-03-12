    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text;

namespace POC_MangoDB_EventHub_trigger
{
    public class MongoDBWebhookTriggers
    {
        private readonly ILogger<MongoDBWebhookTriggers> _logger;

        public MongoDBWebhookTriggers(ILogger<MongoDBWebhookTriggers> logger)
        {
            _logger = logger;
        }


        [Function("MongoDBWebhookTriggers")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            
          string eventHubConnectionString = Environment.GetEnvironmentVariable("EventHub_ConnectionString");

            _logger.LogInformation("MongoDB Atlas Webhook Triggered.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _logger.LogInformation($"Received Payload: {requestBody}");

            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionString);

            try
            {
                Microsoft.Azure.EventHubs.EventData eventData = new Microsoft.Azure.EventHubs.EventData(Encoding.UTF8.GetBytes(requestBody));
                await eventHubClient.SendAsync(eventData);
                _logger.LogInformation("Event sent to Azure Event Hub successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending event: {ex.Message}");
                return new BadRequestObjectResult("Error sending event.");
            }

            return new OkObjectResult("Event successfully sent to Event Hub.");
        }
    }
}
