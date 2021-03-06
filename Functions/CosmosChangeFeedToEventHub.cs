using System;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace BFYOC
{
    public static class CosmosChangeFeedToEventHub
    {
        private static readonly string Container1EventHubName = "container1events";
        private static readonly string OrdersEventHubName = "ordersevents";
        private static readonly string SalesEventHubName = "salesevents";
        [FunctionName("Container1ToEventHub")]
        //[return: EventHub("aggregate", Connection = "myEventHub")]
        public static async Task Container1ToEventHub(
            [CosmosDBTrigger(
                databaseName: "RatingsAPI",
                collectionName: "Container1",
                ConnectionStringSetting = "myCosmosDb",
                LeaseCollectionName = "LeaseCollection1")]IReadOnlyList<Document> Container1input, 
            ILogger log)
            {
            #pragma warning disable CS0618 // Type or member is obsolete
                string eventHubNamespaceConnection = "[INSERT HERE]";
                //string eventHubNamespaceConnection = ConfigurationSettings.AppSettings["myEventHub"];
            #pragma warning restore CS0618 // Type or member is obsolete
                
                // Build connection string to access event hub within event hub namespace.
                EventHubsConnectionStringBuilder eventHubConnectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubNamespaceConnection)
                {
                    EntityPath = Container1EventHubName
                };
                
                // Create event hub client to send change feed events to event hub.
                EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionStringBuilder.ToString());

                foreach (var doc in Container1input)
                {
                    // Convert documents to json.
                    string json = JsonConvert.SerializeObject(doc);
                    EventData data = new EventData(Encoding.UTF8.GetBytes(json));

                    // Use Event Hub client to send the change events to event hub.
                    await eventHubClient.SendAsync(data);
                }
            }
        
        [FunctionName("OrdersToEventHub")]
        //[return: EventHub("aggregate", Connection = "myEventHub")]
        public static async Task OrdersToEventHub(
            [CosmosDBTrigger(
                databaseName: "RatingsAPI",
                collectionName: "Orders",
                ConnectionStringSetting = "myCosmosDb",
                LeaseCollectionName = "LeaseCollection1")]IReadOnlyList<Document> Ordersinput, 
            ILogger log)
            {
            #pragma warning disable CS0618 // Type or member is obsolete
                string eventHubNamespaceConnection = "[INSERT HERE]";
                //string eventHubNamespaceConnection = ConfigurationSettings.AppSettings["myEventHub"];
            #pragma warning restore CS0618 // Type or member is obsolete
                
                // Build connection string to access event hub within event hub namespace.
                EventHubsConnectionStringBuilder eventHubConnectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubNamespaceConnection)
                {
                    EntityPath = OrdersEventHubName
                };
                
                // Create event hub client to send change feed events to event hub.
                EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionStringBuilder.ToString());

                foreach (var doc in Ordersinput)
                {
                    // Convert documents to json.
                    string json = JsonConvert.SerializeObject(doc);
                    EventData data = new EventData(Encoding.UTF8.GetBytes(json));

                    // Use Event Hub client to send the change events to event hub.
                    await eventHubClient.SendAsync(data);
                }
            }
        
        [FunctionName("SalesToEventHub")]
        //[return: EventHub("aggregate", Connection = "myEventHub")]
        public static async Task SalesToEventHub(
            [CosmosDBTrigger(
                databaseName: "RatingsAPI",
                collectionName: "Sales",
                ConnectionStringSetting = "myCosmosDb",
                LeaseCollectionName = "LeaseCollection1")]IReadOnlyList<Document> Salesinput,
            ILogger log)

            {
            #pragma warning disable CS0618 // Type or member is obsolete
                string eventHubNamespaceConnection = "[INSERT HERE]";
                //string eventHubNamespaceConnection = ConfigurationSettings.AppSettings["myEventHub"];
            #pragma warning restore CS0618 // Type or member is obsolete

                // Build connection string to access event hub within event hub namespace.
                EventHubsConnectionStringBuilder eventHubConnectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubNamespaceConnection)
                {
                    EntityPath = SalesEventHubName
                };
                
                // Create event hub client to send change feed events to event hub.
                EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionStringBuilder.ToString());

                foreach (var doc in Salesinput)
                {
                    // Convert documents to json.
                    string json = JsonConvert.SerializeObject(doc);
                    EventData data = new EventData(Encoding.UTF8.GetBytes(json));

                    // Use Event Hub client to send the change events to event hub.
                    await eventHubClient.SendAsync(data);
                }
            }

    }
}
