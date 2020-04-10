using System.Collections.Generic;
using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Replacement;

namespace ToolShed.Services.Factory
{
    public class ReplacementOrderProviderFactory : IReplacementOrderService
    {
        private readonly Dictionary<ReplacementOrderProvider, IReplacementOrderService> references;

        public ReplacementOrderProviderFactory(Dictionary<ReplacementOrderProvider, IReplacementOrderService> references)
        {
            this.references = references;
        }

        public IReplacementOrderService GetReplacementOrderService(ReplacementOrderProvider replacementOrderProvider)
        {
            if (references.TryGetValue(replacementOrderProvider, out var replacementOrderService))
                return replacementOrderService;

            throw new KeyNotFoundException($"The replacement order provider, {replacementOrderProvider}, could not be found.");
        }
    }
}
