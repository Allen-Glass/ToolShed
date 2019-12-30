using System.Threading;
using System.Threading.Tasks;
using Stripe;
using ToolShed.Services.Interfaces.Payment.Stripe;

namespace ToolShed.Services.Payment.Stripe
{
    /// <summary>
    /// Stripe payment wrapper class
    /// </summary>
    public class ChargeServiceWrapper : IChargeServices
    {
        private readonly ChargeService chargeService;

        public ChargeServiceWrapper(ChargeService chargeService)
        {
            this.chargeService = chargeService;
        }
        public async Task<Charge> CaptureAsync(string chargeId, ChargeCaptureOptions chargeCaptureOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return await chargeService.CaptureAsync(chargeId, chargeCaptureOptions, requestOptions, cancellationToken);
        }

        public async Task<Charge> CreateChargeAsync(ChargeCreateOptions chargeCreateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return await chargeService.CreateAsync(chargeCreateOptions, requestOptions, cancellationToken);
        }

        public async Task<Charge> GetAsync(string chargeId, ChargeGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return await chargeService.GetAsync(chargeId, options, requestOptions, cancellationToken);
        }

        public async Task<StripeList<Charge>> ListAsync(ChargeListOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return await chargeService.ListAsync(options, requestOptions, cancellationToken);
        }

        public async Task<Charge> UpdateAsync(string chargeId, ChargeUpdateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default)
        {
            return await chargeService.UpdateAsync(chargeId, options, requestOptions, cancellationToken);
        }
    }
}
