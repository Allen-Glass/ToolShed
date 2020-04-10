using Azure.Storage.Queues;

namespace ToolShed.Services.Interfaces.Queues
{
    public interface ICloudQueue
    {
        /// <summary>
        /// Create azure queue reference
        /// </summary>
        /// <param name="queueUri">uri of azure queue found in azure portal</param>
        /// <returns>queue reference</returns>
        QueueClient CreateQueueClient(string queueUri);
    }
}
