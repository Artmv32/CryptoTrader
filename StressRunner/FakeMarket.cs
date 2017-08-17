using AnalysisModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StressRunner
{
    public sealed class FakeMarket : IMarket
    {
        private List<PriceInfo> _data;

        public DateTime Today { get; set; }

        public FakeMarket(DateTime today)
        {
            Today = today;

            var data = File.ReadAllLines("coindesk-bpi-USD-close_data-2010-07-17_2017-08-17.csv");
            var result = new List<PriceInfo>(data.Length);
            for (int i = 1; i < data.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(data[i]))
                {
                    break;
                }

                var items = data[i].Split(new[] { ',' });
                var date = DateTime.Parse(items[0]);
                var price = decimal.Parse(items[1]);
                result.Add(new PriceInfo
                {
                    Date = date,
                    Price = price,
                });
            }
            _data = result;
        }

        public void Buy(decimal fiatAmount)
        {
            
        }
        
        public void Sell(decimal coinsAmount)
        {
            
        }

        public List<PriceInfo> ProvidePriceInfo(DateTime from)
        {
            return _data.Where(x => x.Date <= Today.Date && x.Date >= from.Date).ToList();
        }
    }
}
