using Microsoft.Azure.Storage.Queue;

namespace ToolShed.Services.Interfaces
{
    public interface ICloudQueue
    {
        /// <summary>
        /// Create azure queue reference
        /// </summary>
        /// <param name="queueName">name of azure queue</param>
        /// <returns>queue reference</returns>
        CloudQueue GetQueueReference(string queueName);
    }
}
