using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisModule.Indicators
{
    public sealed class BoilingerBand
    {
        private readonly SMAIndicator _middleBand = new SMAIndicator(20);
        private List<decimal> _values = new List<decimal>();

        public void Add(decimal value)
        {
            _values.Add(value);
            _middleBand.Add(value);
        }

        public decimal PeekLastMiddle()
        {
            return _middleBand.PeekLast();
        }

        public decimal PeekLastUpper()
        {
            return PeekLastUpper() + (decimal)StandardDeviation(_values, _middleBand.Periods) * 2;
        }

        public decimal PeekLastLower()
        {
            return PeekLastUpper() - (decimal)StandardDeviation(_values, _middleBand.Periods) * 2;
        }

        private static double StandardDeviation(IList<decimal> values, uint period)
        {
            if (values.Count < period)
            {
                return 0;
            }

            uint counter = 0;
            double avg = 0;
            for (int i = values.Count - 1; i >= 0 && counter < period; i--, counter++)
            {
                avg = (avg * counter  + (double)values[i]) / (counter + 1);
            }

            double sum = 0;
            counter = 0;
            for (int i = values.Count - 1; i >= 0 && counter < period; i--, counter++)
            {
                sum += Math.Pow(((double)values[i] - avg), 2);
            }

            return Math.Sqrt(sum / (period - 1));
        }
    }
}
