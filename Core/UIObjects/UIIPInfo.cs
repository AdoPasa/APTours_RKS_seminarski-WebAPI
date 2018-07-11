using Newtonsoft.Json;

namespace Core.UIObjects
{
    public class UIIPInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country_name")]
        public string Country { get; set; }

        [JsonProperty("organisation")]
        public string Org { get; set; }

        [JsonProperty("calling_code")]
        public string CallingCode { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
