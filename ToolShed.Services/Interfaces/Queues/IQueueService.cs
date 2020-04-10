using Azure.Storage.Queues.Models;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Enums;

namespace ToolShed.Services.Interfaces.Queues
{
    public interface IQueueService
    {
        /// <summary>
        /// Send queue a message with the associated user id
        /// </summary>
        /// <param name="queueType">queue being sent the message</param>
        /// <param name="id">unique user identifier</param>
        Task SendMessageAsync(QueueType queueType, string id);

        /// <summary>
        /// Send queue a message
        /// </summary>
        /// <param name="queueType">queue being sent the message</param>
        /// <param name="message">queue message</param>
        Task SendMessageAsync(QueueType queueType, object message);

        /// <summary>
        /// Gets a message from a queue
        /// </summary>
        /// <param name="queueType">queue being sent the message</param>
        /// <param name="cancellation"></param>
        /// <returns>a string representation of the message</returns>
        Task<string> GetMessageAsync(QueueType queueType, CancellationToken cancellation = default);

        /// <summary>
        /// Deletes a given message from the queue
        /// </summary>
        /// <param name="queueType">queue being sent the message</param>
        ///<param name="message"> Given message to delete</param>
        /// <param name="cancellation"></param>
        Task DeleteMessageAsync(QueueType queueType, QueueMessage message, CancellationToken cancellation = default);
    }
}
