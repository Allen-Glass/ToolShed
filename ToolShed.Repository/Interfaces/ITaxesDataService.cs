using System.Threading;
using System.Threading.Tasks;

namespace ToolShed.Repository.Interfaces
{
    public interface ITaxesDataService
    {
        Task<double> GetStateSalesTaxAsync(string state, CancellationToken cancellationToken = default);
    }
}
