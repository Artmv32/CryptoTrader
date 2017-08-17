using AnalysisModule.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalysisModule
{
    public enum TradeAction : byte
    {
        Nothing,
        Buy,
        Sell,
    }

    public class AnalyzerState
    {
        public decimal Coins { get; set; }

        public decimal FiatMoney { get; set; }

        public decimal FiatChunkSize { get; set; }

        public decimal CoinsSize { get; set; }

        public TradeAction LastTradeAction { get; set; }

        public DateTime LastRunDate { get; set; }

        public decimal LastPrice { get; set; }

        public float LastGrowthStrength { get; set; }

        public float LastFallStrength { get; set; }
    }

    public class Analyzer
    {
        private readonly IMarket _market;
        private readonly BoilingerBand boilinger = new BoilingerBand();
        private readonly EMAIndicator[] emas;

        public event Action<Analytics> OnReportAnalytics;

        public Analyzer(IMarket market)
        {
            _market = market;
            var periods = new[] { 5, 10, 20, 30, 50, 80, 100 };
            emas = new EMAIndicator[periods.Length];
            for (int i = 0; i < emas.Length; i++)
            {
                emas[i] = new EMAIndicator((uint)periods[i]);
            }
        }

        public void Run()
        {
            var data = GetPriceHistory();
            var today = GetToday();
            var oldState = LoadState();
            var newState = new AnalyzerState();

            TradeAction tradeAction = TradeAction.Nothing;

            DateTime prevData;
            decimal todayPrice;
            ProcessHistoricalData(data, out prevData, out todayPrice);

            var analytics = new Analytics
            {
                DateTime = today,
                BoilingerUpper = boilinger.PeekLastUpper(),
                BoilingerMiddle = boilinger.PeekLastMiddle(),
                BoilingerLower = boilinger.PeekLastLower(),
                Price = todayPrice,
                EMAs = emas.Select(x => x.PeekLast()).ToArray(),
                State = oldState,
            };

            if (prevData.Date == today.Date)
            {
                int countAbove = 0;
                int countBelow = 0;
                foreach (var ema in emas)
                {
                    if (todayPrice > ema.PeekLast())
                    {
                        countAbove++;
                    }
                    else if (todayPrice < ema.PeekLast())
                    {
                        countBelow--;
                    }
                }
                float growthStrength = countAbove / (float)emas.Length;
                float fallStrength = countBelow / (float)emas.Length;
                newState.LastGrowthStrength = analytics.GrowthStrength = growthStrength;
                newState.LastFallStrength = analytics.FallStrength = fallStrength;

                decimal priceDiff = oldState.LastPrice - todayPrice;

                if (boilinger.PeekLastUpper() < todayPrice) // Fast growth
                {
                    analytics.Comment("Boilinger: fast growth.");
                    analytics.Comment(string.Format("Price diff: {0:C2}, margin: {1:C2}", priceDiff, priceDiff * oldState.Coins));

                }
                else if (boilinger.PeekLastLower() > todayPrice) // Fast fall
                {
                    analytics.Comment("Boilinger: fast fall.");
                    analytics.Comment(string.Format("Price diff: {0:C2}, margin: {1:C2}", priceDiff, priceDiff * oldState.Coins));

                }
                else // inside range
                {
                    analytics.Comment("Boilinger: inside range.");

                }                
            }

            newState.LastPrice = todayPrice;            
            SaveState(newState);

            analytics.Coins = newState.Coins;
            analytics.Dollars = newState.FiatMoney;
            analytics.CoinsWorth = newState.Coins * todayPrice;
            analytics.TradeAction = tradeAction;

            if (OnReportAnalytics != null)
            {
                OnReportAnalytics(analytics);
            }
        }

        private void ProcessHistoricalData(List<PriceInfo> data, out DateTime prevData, out decimal prevPrice)
        {
            prevData = DateTime.Now.AddYears(-100);
            prevPrice = 0;
            foreach (var item in data)
            {
                if ((item.Date - prevData).TotalDays < 1)
                {
                    continue;
                }
                prevData = item.Date;
                prevPrice = item.Price;
                boilinger.Add(item.Price);
                foreach (var ema in emas)
                {
                    ema.Add(item.Price);
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

        protected virtual AnalyzerState LoadState()
        {
            return new AnalyzerState();
        }

        protected virtual void SaveState(AnalyzerState state)
        {

        }
    }
}
