using System;
using System.Collections.Generic;

namespace AnalysisModule
{
    public sealed class Analytics
    {
        public DateTime DateTime { get; set; }

        public decimal Price { get; set; }

        public decimal Coins { get; set; }

        public decimal Dollars { get; set; }

        public decimal CoinsWorth { get; set; }

        public decimal BoilingerUpper { get; set; }

        public decimal BoilingerMiddle { get; set; }

        public decimal BoilingerLower { get; set; }

        public decimal[] EMAs { get; set; }

        public float GrowthStrength { get; set; }

        public float FallStrength { get; set; }

        public List<string> Comments { get; set; }

        public AnalyzerState State { get; set; }

        public TradeAction TradeAction { get; set; }

        public Analytics()
        {
            Comments = new List<string>();
        }
        
        public void Comment(string comment)
        {
            Comments.Add(comment);
        }
    }
}
