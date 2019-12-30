using Microsoft.Azure.Storage.Queue;
using ToolShed.Models.Enums;

namespace ToolShed.Services.Interfaces.Factory
{
    public interface IQueueFactory
    {
        CloudQueue CreateQueueClient(QueueType queueType);
    }
}
