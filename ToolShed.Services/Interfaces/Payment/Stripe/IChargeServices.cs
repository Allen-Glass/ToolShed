using Stripe;
using System.Threading;
using System.Threading.Tasks;

namespace ToolShed.Services.Interfaces.Payment.Stripe
{
    public interface IChargeServices
    {
        /// <summary>
        /// Capture a stripe charge <seealso cref="https://stripe.com/docs/api/charges"/>
        /// </summary>
        /// <param name="chargeId">unique id of the charge</param>
        /// <param name="chargeCaptureOptions"></param>
        /// <param name="requestOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>charge object</returns>
        Task<Charge> CaptureAsync(string chargeId, ChargeCaptureOptions chargeCaptureOptions = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a stripe charge <seealso cref="https://stripe.com/docs/api/charges"/>
        /// </summary>
        /// <param name="chargeCreateOptions"></param>
        /// <param name="requestOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>charge object</returns>
        Task<Charge> CreateChargeAsync(ChargeCreateOptions chargeCreateOptions, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a stripe charge <seealso cref="https://stripe.com/docs/api/charges"/>
        /// </summary>
        /// <param name="chargeId">unique id of the charge</param>
        /// <param name="options"></param>
        /// <param name="requestOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>charge object</returns>
        Task<Charge> GetAsync(string chargeId, ChargeGetOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a stripe charge <seealso cref="https://stripe.com/docs/api/charges"/>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="requestOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>list of charge objects</returns>
        Task<StripeList<Charge>> ListAsync(ChargeListOptions options = null, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a stripe charge <seealso cref="https://stripe.com/docs/api/charges"/>
        /// </summary>
        /// <param name="chargeId">unique id of the charge</param>
        /// <param name="options"></param>
        /// <param name="requestOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>charge object</returns>
        Task<Charge> UpdateAsync(string chargeId, ChargeUpdateOptions options, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
    }
}
