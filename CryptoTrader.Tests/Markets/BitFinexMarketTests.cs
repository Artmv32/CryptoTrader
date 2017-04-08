using CryptoTrader.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CryptoTrader.Tests.Markets
{
    public class BitFinexMarketTests
    {
        private BitFinexMarket _target = new BitFinexMarket();

        [Fact]
        public void RunTest()
        {
            var ticker = _target.GetTicker();
        }
    }
}
