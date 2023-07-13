using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Services.Authentication.External;

namespace Nop.Plugin.ExternalAuth.Twitter.Infrastructure
{
    /// <summary>
    /// Represents registrar of Twitter authentication service
    /// </summary>
    public class TwitterAuthenticationRegistrar : IExternalAuthenticationRegistrar
    {
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="builder">Authentication builder</param>
        public void Configure(AuthenticationBuilder builder)
        {
            builder.AddTwitter(TwitterDefaults.AuthenticationScheme, options =>
            {
                //set credentials
                var settings = EngineContext.Current.Resolve<TwitterExternalAuthSettings>();
                options.ConsumerKey = string.IsNullOrEmpty(settings?.ApiKey) ? nameof(options.ConsumerKey) : settings.ApiKey;
                options.ConsumerSecret = string.IsNullOrEmpty(settings?.ApiSecret) ? nameof(options.ConsumerSecret) : settings.ApiSecret;

                //store access and refresh tokens for the further usage
                options.SaveTokens = true;

                //set custom events handlers
                options.Events = new TwitterEvents
                {
                    //in case of error, redirect the user to the specified URL
                    OnRemoteFailure = context =>
                    {
                        context.HandleResponse();

                        var errorUrl = context.Properties.GetString(TwitterAuthenticationDefaults.ErrorCallback);
                        context.Response.Redirect(errorUrl);

                        return Task.FromResult(0);
                    }
                };
            });
        }
    }
}