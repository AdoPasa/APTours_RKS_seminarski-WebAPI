using Core.UIObjects;
using Newtonsoft.Json;
using System;
using System.Net;

namespace API.Helpers
{
    public static class IPHelper
    {
        public static string GetLanguageCulture(string ipAddress) {
            try
            {
                string jsonClientIpInfo = new WebClient().DownloadString("https://api.ipdata.co/" + ipAddress);
                UIIPInfo ipInfo = JsonConvert.DeserializeObject<UIIPInfo>(jsonClientIpInfo);

                if (ipInfo != null)
                {
                    return ipInfo.Country == "Croatia" || ipInfo.Country == "Bosnia and Herzegovina" || ipInfo.Country == "Serbia" ? "bs-Latn-BA" : "en";
                } 
            }
            catch (Exception ex){  }

            return "";
        }
    }
}
