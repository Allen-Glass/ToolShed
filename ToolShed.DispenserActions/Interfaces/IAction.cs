using System.Threading;
using System.Threading.Tasks;

namespace ToolShed.DispenserActions.Interfaces
{
    /// <summary>
    /// Commands targeting dispensers. Releasing items for rental. Returns. Dispensing
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Sending an action to a dispenser
        /// </summary>
        /// <param name="items"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PlaceAction(string items, CancellationToken cancellationToken);
    }
}
