using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;

namespace CryptoTrader.Core.Markets
{
    public class PriceInfo
    {
        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }

        [JsonProperty("last_updated")]
        public long LastUpdated { get; set; }

        public DateTime Time { get; set; }
    }

    public class WhaleClubBalance
    {
        [JsonProperty("available_amount")]
        public int AvailableAmount { get; set; }

        [JsonProperty("total_amount")]
        public int TotalAmount { get; set; }

        [JsonProperty("unconfirmed_amount")]
        public int UnconfirmedAmount { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("active_amount")]
        public Dictionary<string, int> ActiveAmount { get; set; }

        [JsonProperty("pending_amount")]
        public Dictionary<string, int> PendingAmount { get; set; }

        [JsonProperty("active_amount_turbo")]
        public Dictionary<string, int> ActiveAmountTurbo { get; set; }

        [JsonProperty("last_updated")]
        public long LastUpdated { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public class Balance
    {
        public decimal AvailableAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal UnconfirmedAmount { get; set; }

        public string Address { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Currency { get; set; }
    }

    public class Whaleclub
    {
        private Uri EndPoint = new Uri("https://api.whaleclub.co/v1/");
        private const string ApiToken = "3f35c5ff-defb-4683-97ca-14bd11a8104f";
        private const string AuthToken = "Authorization: Bearer " + ApiToken;

        public void RequestMarket()
        {
            var client = new RestClient(EndPoint);
            var request = new RestRequest("price/{pair}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + ApiToken);
            request.AddUrlSegment("pair", "BTC-USD");

            var response = client.Execute(request);
        }

        public Balance GetBalance()
        {
            var client = new RestClient(EndPoint);
            var request = new RestRequest("balance", Method.GET);
            request.AddHeader("Authorization", "Bearer " + ApiToken);
            var response = client.Execute(request);

            var data = JsonConvert.DeserializeObject<WhaleClubBalance>(response.Content);

            var result = new Balance
            {
                AvailableAmount = data.AvailableAmount * Constants.OneSatoshi,
                TotalAmount = data.TotalAmount * Constants.OneSatoshi,
                UnconfirmedAmount = data.UnconfirmedAmount * Constants.OneSatoshi,
                Address = data.Address,
                LastUpdated = FromNumber(data.LastUpdated),
                Currency = data.Currency,
            };
            return result;
        }

        private static DateTime FromNumber(long number)
        {
            var t = DateTime.FromFileTime(number);
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, t.Hour, t.Minute, t.Second, t.Millisecond);
        }

        public Dictionary<string, PriceInfo> GetPrice(string pair)
        {
            return GetPrice(new[] { pair });
        }

        public Dictionary<string, PriceInfo> GetPrice(string[] pairs)
        {
            if (pairs.Length == 0 || pairs.Length > 5)
                throw new ArgumentException("Provide from 1 to 5 pairs.", "pairs");

            var client = new RestClient(EndPoint);
            var request = new RestRequest("price/{pair}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + ApiToken);
            request.AddUrlSegment("pair", "BTC-USD");

            var response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<Dictionary<string, PriceInfo>>(response.Content);
            foreach (var item in data)
            {
                var info = item.Value;
                info.Time = FromNumber(info.LastUpdated);
            }
            return data;
        }
    }
}
