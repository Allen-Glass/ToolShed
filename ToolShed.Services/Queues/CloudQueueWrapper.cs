using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services.Queues
{
    public class CloudQueueWrapper : ICloudQueue
    {
        private readonly CloudStorageAccount cloudStorageAccount;

        public CloudQueueWrapper(CloudStorageAccount cloudStorageAccount)
        {
            this.cloudStorageAccount = cloudStorageAccount;
        }

        /// <summary>
        /// Create azure queue reference
        /// </summary>
        /// <param name="queueName">name of azure queue</param>
        /// <returns>queue reference</returns>
        public CloudQueue GetQueueReference(string queueName)
        {
            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            return cloudQueueClient.GetQueueReference(queueName);
        }
    }
}
