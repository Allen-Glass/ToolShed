using Azure.Storage.Queues;
using System.Collections.Generic;
using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Queues;
using ToolShed.Services.Interfaces.Factory;

namespace ToolShed.Services.Factory
{
    public class QueueFactory : IQueueFactory
    {
        private readonly IDictionary<QueueType, string> queueReferences;
        private readonly ICloudQueue cloudQueue;
        private readonly string baseUrl;

        public QueueFactory(IDictionary<QueueType, string> queueReferences,
            ICloudQueue cloudQueue,
            string baseUrl)
        {
            this.queueReferences = queueReferences;
            this.cloudQueue = cloudQueue;
            this.baseUrl = baseUrl;
        }

        /// <summary>
        /// Create a connection to a specific azure storage queue based on type
        /// </summary>
        /// <param name="queueType">Queue name</param>
        /// <returns>Cloud queue class for sending and receiving messages from queue</returns>
        public QueueClient GetQueueReference(QueueType queueType)
        {
            if (queueReferences.TryGetValue(queueType, out var cloudQueueName))
                return cloudQueue.CreateQueueClient($"{baseUrl}/{cloudQueueName}");

            throw new KeyNotFoundException(nameof(queueType));
        }
    }
}
