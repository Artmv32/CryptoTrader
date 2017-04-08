using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrader.Markets
{
    public class MarketBase
    {
        public virtual string MakeOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
