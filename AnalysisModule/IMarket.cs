using System;
using System.Collections.Generic;

namespace AnalysisModule
{
    public interface IMarket
    {
        void Buy(decimal fiatAmount);

        void Sell(decimal coinsAmount);

        List<PriceInfo> ProvidePriceInfo(DateTime from);
    }
}
