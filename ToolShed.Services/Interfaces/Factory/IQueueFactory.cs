using Azure.Storage.Queues;
using ToolShed.Models.Enums;

namespace ToolShed.Services.Interfaces.Factory
{
    public interface IQueueFactory
    {
        QueueClient GetQueueReference(QueueType queueType);
    }
}
