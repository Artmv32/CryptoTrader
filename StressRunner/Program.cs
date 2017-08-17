using System;
using AnalysisModule;
using System.Collections.Generic;

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
            var dates = new List<DateTime>();
            {
                var date = new DateTime(2010, 7, 18);
                for (int i = 0; i < 2590; i++) // So far historical data
                {
                    dates.Add(date);
                    date = date.AddDays(1);
                }
            }

            var rnd = new Random();
            for (int simulation = 0; simulation < 10; simulation++)
            {
                var startI = rnd.Next(dates.Count);
                var date = dates[startI];
                var market = new FakeMarket(date);
                for (int i = startI; i < dates.Count; i++)
                {
                    var analyzer = new StubAnalyzer(market)
                    {
                        Today = date
                    };
                    market.Today = analyzer.Today;
                    analyzer.Run();
                    date = date.AddDays(1);
                }               
            }            
        }
    }
}
