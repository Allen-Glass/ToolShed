using Microsoft.Azure.Storage.Queue;
using System.Collections.Generic;
using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces;
using ToolShed.Services.Interfaces.Factory;

namespace ToolShed.Services.Factory
{
    public class QueueFactory : IQueueFactory
    {
        private readonly Dictionary<QueueType, string> references;
        private readonly ICloudQueue cloudQueue;

        public QueueFactory(Dictionary<QueueType, string> references,
            ICloudQueue cloudQueue)
        {
            this.references = references;
        }

        public CloudQueue CreateQueueClient(QueueType queueType)
        {
            if (references.TryGetValue(queueType, out var queueReference))
                return cloudQueue.GetQueueReference(queueReference);

            throw new KeyNotFoundException(nameof(QueueType));
        }
    }
}
