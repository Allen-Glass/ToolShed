using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Place order
        /// </summary>
        /// <param name="order">user order</param>
        Task PlaceOrderAsync(Order order);
    }
}
