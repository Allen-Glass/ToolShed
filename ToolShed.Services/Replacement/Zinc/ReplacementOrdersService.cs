using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Services.Interfaces.Factory;

namespace ToolShed.Services.Replacement.Zinc
{
    public class ReplacementOrdersService
    {
        private readonly IReplacementOrderProviderFactory replacementOrderProviderFactory;

        public ReplacementOrdersService(IReplacementOrderProviderFactory replacementOrderProviderFactory)
        {
            this.replacementOrderProviderFactory = replacementOrderProviderFactory;
        }

        public async Task PlaceReoccuringOrderAsync()
        {

        }
    }
}
