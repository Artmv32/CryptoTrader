using System;

namespace CryptoTrader.Analysis
{
    public class PriceListener
    {
        public virtual void OnPriceUpdate(DateTime dateTime, decimal price)
        {

        }
    }
}
