using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Replacement;

namespace ToolShed.Services.Interfaces.Factory
{
    public interface IReplacementOrderProviderFactory
    {
        IReplacementOrderService GetReplacementOrderService(ReplacementOrderProvider replacementOrderProvider);
    }
}
