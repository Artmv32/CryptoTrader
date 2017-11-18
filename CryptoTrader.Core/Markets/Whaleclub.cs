using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrader.Core.Markets
{
    public class Balance
    {
        public int AvailableAmount { get; set; }

        public int TotalAmount { get; set; }

        public int UnconfirmedAmount { get; set; }

        public string DepositAddress { get; set; }

        public object ActiveAmount { get; set; }

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
        
        public void GetBalance()
        {
            var client = new RestClient(EndPoint);
            var request = new RestRequest("balance", Method.GET);
            request.AddHeader("Authorization", "Bearer " + ApiToken);
            var response = client.Execute(request);

        }
    }
}
