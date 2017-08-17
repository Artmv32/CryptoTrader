using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisModule.Indicators
{
    public sealed class SMAIndicator
    {
        private readonly List<decimal> _values = new List<decimal>();
        private readonly List<decimal> _smas = new List<decimal>();
        
        public bool IsInitialized
        {
            get { return _values.Count >= Periods; }
        }

        public uint Periods { get; private set; }

        public SMAIndicator(uint periods = 10)
        {
            Periods = periods;
        }

        public decimal PeekLast()
        {
            if (IsInitialized)
            {
                return _smas.Last();
            }
            return 0;
        }

        public void Add(decimal value)
        {
            _values.Add(value);

            uint counter = 0;
            decimal avg = 0;
            for (int i = _values.Count - 1; counter < Periods && i >= 0; i--, counter++)
            {
                avg = (avg * (counter) + _values[i]) / (counter + 1);
            }
            _smas.Add(avg);
        }
    }
}
