using Nop.Core;
using Nop.Plugin.ExternalAuth.Twitter.Components;
using Nop.Services.Authentication.External;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.ExternalAuth.Twitter
{
    /// <summary>
    /// Represents method for the authentication with Twitter account
    /// </summary>
    public class TwitterAuthenticationMethod : BasePlugin, IExternalAuthenticationMethod
    {
        #region Fields

        protected readonly ILocalizationService _localizationService;
        protected readonly ISettingService _settingService;
        protected readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public TwitterAuthenticationMethod(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/TwitterAuthentication/Configure";
        }

        /// <summary>
        /// Gets a type of a view component for displaying plugin in public store
        /// </summary>
        /// <returns>View component type</returns>
        public Type GetPublicViewComponent()
        {
            return typeof(TwitterAuthenticationViewComponent);
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            //settings
            await _settingService.SaveSettingAsync(new TwitterExternalAuthSettings());

            //locales
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.ExternalAuth.Twitter.ApiKey"] = "API Key",
                ["Plugins.ExternalAuth.Twitter.ApiKey.Hint"] = "Enter your app API key here. You can find it on your Twitter developer portal page.",
                ["Plugins.ExternalAuth.Twitter.ApiSecret"] = "API Secret",
                ["Plugins.ExternalAuth.Twitter.ApiSecret.Hint"] = "Enter your API secret here. You can find it on your Twitter developer portal page.",
                ["Plugins.ExternalAuth.Twitter.Instructions"] = "<p>To configure authentication with Twitter, please visit<a href=\"https://https://developer.twitter.com\" target =\"_blank\"> Twitter Developers Portal</a> page.And Copy your API Key and API secret below.<br/><br/></p>"
            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            //settings
            await _settingService.DeleteSettingAsync<TwitterExternalAuthSettings>();

            //locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.ExternalAuth.Twitter");

            await base.UninstallAsync();
        }

        #endregion
    }
}