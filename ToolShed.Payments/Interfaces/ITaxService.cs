using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Payments.Interfaces
{
    public interface ITaxService
    {
        Task<Payment> AppendSalesTaxAsync(Payment payment, string state);

        Task<double> GetSalesTaxAsync(Payment payment, string state);
    }
}
