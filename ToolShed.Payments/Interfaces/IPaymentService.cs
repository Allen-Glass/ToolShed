using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Payments.Interfaces
{
    public interface IPaymentService
    {
        Task<Rental> CalculateRentalPriceAsync(Rental rental, string state);
    }
}
