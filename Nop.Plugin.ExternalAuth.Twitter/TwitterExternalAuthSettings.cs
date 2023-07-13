using Nop.Core.Configuration;

namespace Nop.Plugin.ExternalAuth.Twitter
{
    /// <summary>
    /// Represents settings of the Twitter authentication method
    /// </summary>
    public class TwitterExternalAuthSettings : ISettings
    {
        /// <summary>
        /// Gets or sets OAuth2 Api Key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets OAuth2 Api Secret
        /// </summary>
        public string ApiSecret { get; set; }
    }
}