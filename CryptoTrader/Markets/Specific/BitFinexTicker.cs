using RestSharp.Deserializers;
using System;

namespace CryptoTrader.Markets.Specific
{
    public class BitFinexTicker
    {

        [DeserializeAs(Name = "mid")]
        public decimal Mid { get; set; }

        [DeserializeAs(Name = "bid")]
        public decimal Bid { get; set; }

        [DeserializeAs(Name = "ask")]
        public decimal Ask { get; set; }

        [DeserializeAs(Name = "last_price")]
        public decimal LastPrice { get; set; }

        [DeserializeAs(Name = "low")]
        public decimal Low { get; set; }

        [DeserializeAs(Name = "high")]
        public decimal High { get; set; }

        [DeserializeAs(Name = "volume")]
        public decimal Volume { get; set; }

        [DeserializeAs(Name = "timeStamp")]
        public string TimeStamp { get; set; }

        public DateTime Time
        {
            get { return DateTime.FromFileTimeUtc(long.Parse(TimeStamp.Replace(".", ""))); }
        }
    }
}
