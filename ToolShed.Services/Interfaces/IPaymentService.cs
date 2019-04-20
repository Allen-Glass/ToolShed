using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Rental> CalculateRentalPriceAsync(Rental rental, string state);
    }
}
