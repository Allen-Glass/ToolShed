using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolShed.DispenserActions.Interfaces
{
    public interface IDispense
    {
        Task DispenseTools(string items, CancellationToken cancellationToken);
    }
}
