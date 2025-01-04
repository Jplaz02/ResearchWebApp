using Syncfusion.Blazor;
using System.Resources;

namespace ResearchWebApp.Services
{
    public class CustomSyncfusionStringLocalizer : ISyncfusionStringLocalizer
    {
        // Implement the ResourceManager property
        public ResourceManager ResourceManager { get; private set; }

        // Constructor to initialize the ResourceManager (if needed)
        public CustomSyncfusionStringLocalizer()
        {
            // Initialize the ResourceManager with a suitable resource file if you have one.
            // For now, it's set to null as we are not using any resource files.
            ResourceManager = null;
        }

        // Implement the GetText method
        public string GetText(string key)
        {
            // Return the key itself, you can customize this to return localized strings.
            return key;
        }
    }
}
