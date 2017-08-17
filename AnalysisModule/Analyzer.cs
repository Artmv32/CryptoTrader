using AnalysisModule.Indicators;
using System;
using System.Collections.Generic;

namespace AnalysisModule
{
    public struct PriceInfo
    {
        public DateTime Date { get; set; }

        public decimal Price { get; set; }
    }

    public interface IMarket
    {
        void Buy(decimal fiatAmount);

        void Sell(decimal coinsAmount);

        List<PriceInfo> ProvidePriceInfo(DateTime from);
    }

    public interface ITransactionsBook
    {

    }

    public class Analyzer
    {
        private readonly IMarket _market;

        public Analyzer(IMarket market)
        {
            _market = market;
        }

        public void Run()
        {
            var data = GetPriceHistory();

            var boilinger = new BoilingerBand();
            var ema1 = new EMAIndicator(20);
            var ema2 = new EMAIndicator(40);
            var ema3 = new EMAIndicator(80);
            var ema4 = new EMAIndicator(120);

            DateTime prevData = DateTime.Now.AddYears(-100);
            decimal prevPrice = 0;
            foreach (var item in data)
            {
                if ((item.Date - prevData).TotalDays < 1)
                {
                    continue;
                }
                prevData = item.Date;
                prevPrice = item.Price;
                boilinger.Add(item.Price);
                ema1.Add(item.Price);
                ema2.Add(item.Price);
                ema3.Add(item.Price);
                ema4.Add(item.Price);
            }

            if (prevData.Date == GetToday().Date)
            {
                if (boilinger.PeekLastUpper() < prevPrice) // Fast growth
                {

                }
                else if (boilinger.PeekLastLower() > prevPrice) // Fast fall
                {

                }
                else // inside range
                {

                }
            }
        }

        protected virtual List<PriceInfo> GetPriceHistory()
        {
            return new List<PriceInfo>();
        }

        protected virtual DateTime GetToday()
        {
            return DateTime.Now;
        }
    }
}
