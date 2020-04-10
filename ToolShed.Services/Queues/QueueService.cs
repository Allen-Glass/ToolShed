using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Factory;
using ToolShed.Services.Interfaces.Queues;

namespace ToolShed.Services.Queues
{
    public class QueueService : IQueueService
    {
        private readonly IQueueFactory queueFactory;
        private readonly ILogger<IQueueService> logger;

        public QueueService(IQueueFactory queueFactory,
            ILogger<IQueueService> logger)
        {
            this.queueFactory = queueFactory;
            this.logger = logger;
        }

        public async Task SendMessageAsync(QueueType queueType, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var queueClient = queueFactory.GetQueueReference(queueType);
            await queueClient.SendMessageAsync(id);
        }

        /// <summary>
        /// Send queue a message
        /// </summary>
        /// <param name="queueType">queue being sent the message</param>
        /// <param name="message">queue message</param>
        public async Task SendMessageAsync(QueueType queueType, object message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var queueClient = queueFactory.GetQueueReference(queueType);
            var queueMessage = JsonConvert.SerializeObject(message);
            try
            {
                await queueClient.SendMessageAsync(queueMessage);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to send message to queue.");
                throw;
            }
        }

        /// <summary>
        /// Gets a message from a queue
        /// </summary>
        /// <param name="queueType">queue being sent the message</param>
        /// <param name="cancellation"></param>
        /// <returns>a string representation of the message</returns>
        public async Task<string> GetMessageAsync(QueueType queueType, CancellationToken cancellation = default)
        {
            var queueClient = queueFactory.GetQueueReference(queueType);
            QueueMessage message = null;
            try
            {
                var messageResponse = await queueClient.ReceiveMessagesAsync(maxMessages: 1, visibilityTimeout: null, cancellationToken: cancellation);
                message = messageResponse.Value.First();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to receive message from queue.");
                throw;
            }

            await DeleteMessageAsync(queueType, message);
            return message.MessageText;
        }

        /// <summary>
        /// Deletes a given message from the queue
        /// </summary>
        /// <param name="queueType">queue being sent the message</param>
        ///<param name="message"> Given message to delete</param>
        /// <param name="cancellation"></param>
        public async Task DeleteMessageAsync(QueueType queueType, QueueMessage message, CancellationToken cancellation = default)
        {
            var queueClient = queueFactory.GetQueueReference(queueType);
            try
            {
                await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt, cancellation);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to delete message from queue.");
                throw;
            }
        }
    }
}
