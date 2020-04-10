using Azure.Core;
using Azure.Storage.Queues;
using System;
using ToolShed.Services.Interfaces.Queues;

namespace ToolShed.Services.Queues
{
    public class QueueClientWrapper : ICloudQueue
    {
        private readonly TokenCredential tokenCredential;

        /// <summary>
        /// Create new instance of QueueClientWrapper with TokenCredential parameter
        /// </summary>
        /// <param name="tokenCredential">authentication token</param>
        public QueueClientWrapper(TokenCredential tokenCredential)
        {
            this.tokenCredential = tokenCredential;
        }

        /// <summary>
        /// Create azure queue reference
        /// </summary>
        /// <param name="queueUri">uri of azure queue found in azure portal</param>
        /// <returns>queue reference</returns>
        public QueueClient CreateQueueClient(string queueUri)
        {
            return new QueueClient(new Uri(queueUri), tokenCredential);
        }
    }
}
