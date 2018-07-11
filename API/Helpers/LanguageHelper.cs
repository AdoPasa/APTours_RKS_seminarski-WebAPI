using System.Globalization;
using System.Threading;

namespace API.Helpers
{
    public static class LanguageHelper
    {
        public static void SetLanguage(string lang)
        {
            var culture = new CultureInfo(lang);
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = culture; 
        }
    }
}
