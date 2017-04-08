using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrader.Markets.Specific
{
    public sealed class BitFinexMarket : MarketBase
    {
        private const string Key = "";
        private const string Url = "https://api.bitfinex.com/v1";
        private readonly RestClient _client;

        public BitFinexMarket()
        {
            _client = new RestClient(Url);
        }

        public BitFinexTicker GetTicker()
        {
            var request = new RestRequest("pubticker/{pair}", Method.GET);
            request.AddUrlSegment("pair", "btcusd");
            var response = _client.Execute<BitFinexTicker>(request);
            return response.Data;
        }
    }
}
