using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToolShed.Repository.Interfaces
{
    public interface ITaxesDataService
    {
        Task<double> GetStateSalesTaxAsync(string state);
    }
}
