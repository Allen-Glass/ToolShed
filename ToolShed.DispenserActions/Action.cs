using System;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.DispenserActions.Interfaces;

namespace ToolShed.DispenserActions
{
    public class Action : IAction
    {
        public async Task PlaceAction(string items, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
