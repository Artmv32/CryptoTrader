using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrader.Markets
{
    public enum Side : byte
    {
        Buy,
        Sell,
    }

    public enum OrderType : byte
    {
        Market,
        Limit,
        Stop,
        TrailingStop,
        FillOrKill, 
        ExchangeMarket,
        ExchangeLimit,
        ExchangeStop,
        ExchangeTrailingStop,
        ExchangeFillOrKill,
    }

    public class Order
    {
        public string Symbol { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        public Side Side { get; set; }

        public bool IsHidden { get; set; }

        public bool IsPostOnly { get; set; }
    }
}
