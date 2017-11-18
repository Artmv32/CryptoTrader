using System;
using AnalysisModule;
using System.Collections.Generic;
using CryptoTrader.Core.Markets;

namespace StressRunner
{
    /// <summary>
    /// What if server will return wrong data like 0.0?
    /// What if crypto will suddenly crash like down to 10$?
    /// Buy low and sell high.
    /// </summary>
    class Program
    {
        public class StubAnalyzer : Analyzer
        {
            public DateTime Today { get; set; }

            public StubAnalyzer(IMarket market) : base(market)
            {
            }

            protected override DateTime GetToday()
            {
                return Today;
            }            
        }

        static void Main(string[] args)
        {
            long time = 1510970802;
            var a = TimeSpan.FromMilliseconds(time);
            var d = DateTime.FromFileTime(time);
            var market = new Whaleclub();
            market.GetBalance();
        }
    }
}
