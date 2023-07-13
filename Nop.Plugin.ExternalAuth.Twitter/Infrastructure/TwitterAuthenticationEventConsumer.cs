using System.Security.Claims;
using Nop.Services.Authentication.External;
using Nop.Services.Customers;
using Nop.Services.Events;

namespace Nop.Plugin.ExternalAuth.Twitter.Infrastructure
{
    /// <summary>
    /// Twitter authentication event consumer (used for saving customer fields on registration)
    /// </summary>
    public class TwitterAuthenticationEventConsumer : IConsumer<CustomerAutoRegisteredByExternalMethodEvent>
    {
        #region Fields

        protected readonly ICustomerService _customerService;

        #endregion

        #region Ctor

        public TwitterAuthenticationEventConsumer(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handle event
        /// </summary>
        /// <param name="eventMessage">Event message</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task HandleEventAsync(CustomerAutoRegisteredByExternalMethodEvent eventMessage)
        {
            if (eventMessage?.Customer == null || eventMessage.AuthenticationParameters == null)
                return;

            //handle event only for this authentication method
            if (!eventMessage.AuthenticationParameters.ProviderSystemName.Equals(TwitterAuthenticationDefaults.SystemName))
                return;

            var customer = eventMessage.Customer;
            //store some of the customer fields
            var email = eventMessage.AuthenticationParameters.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(email))
                customer.Email = email;

            var firstName = eventMessage.AuthenticationParameters.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value;
            if (!string.IsNullOrEmpty(firstName))
                customer.FirstName = firstName;

            var lastName = eventMessage.AuthenticationParameters.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value;
            if (!string.IsNullOrEmpty(lastName))
                customer.LastName = lastName;

            await _customerService.UpdateCustomerAsync(customer);
        }

        #endregion
    }
}