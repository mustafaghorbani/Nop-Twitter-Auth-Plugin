using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.ExternalAuth.Twitter.Models
{
    /// <summary>
    /// Represents plugin configuration model
    /// </summary>
    public record ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("Plugins.ExternalAuth.Twitter.ApiKey")]
        public string ApiKey { get; set; }

        [NopResourceDisplayName("Plugins.ExternalAuth.Twitter.ApiSecret")]
        public string ApiSecret { get; set; }
    }
}