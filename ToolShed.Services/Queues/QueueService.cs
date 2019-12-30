using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Factory;

namespace ToolShed.Services.Queues
{
    public class QueueService
    {
        private readonly IQueueFactory queueFactory;

        public QueueService(IQueueFactory queueFactory)
        {
            this.queueFactory = queueFactory;
        }

        public async Task SendMessageAsync(QueueType queueType, object message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var queueClient = queueFactory.CreateQueueClient(queueType);
            var cloudQueueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(message));
            await queueClient.AddMessageAsync(cloudQueueMessage);
        }

        public async Task<string> GetMessageAsync(QueueType queueType)
        {
            var queueClient = queueFactory.CreateQueueClient(queueType);
            var cloudQueueMessage = await queueClient.GetMessageAsync();
            return cloudQueueMessage.AsString;
        }
    }
}
