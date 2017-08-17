using System.Collections.Generic;
using System.Linq;

namespace AnalysisModule.Indicators
{
    public sealed class EMAIndicator
    {
        private readonly SMAIndicator _sma;
        private uint _periods;
        private decimal _multiplier;
        private readonly List<decimal> _emas = new List<decimal>();

        public bool IsInitialized
        {
            get { return _emas.Any(); }
        }

        public EMAIndicator(uint periods = 10)
        {
            _periods = periods;
            _multiplier = 2M / (decimal)(periods + 1);
            _sma = new SMAIndicator(10);
        }

        public decimal PeekLast()
        {
            if (IsInitialized)
            {
                return _emas.Last();
            }
            return 0;
        }

        public void Add(decimal value)
        {
            bool wasInit = _sma.IsInitialized;
            if (!_sma.IsInitialized)
            {
                _sma.Add(value);
            }
            if (wasInit != _sma.IsInitialized)
            {
                _emas.Add(_sma.PeekLast());
            }
            else if (_emas.Any())
            {
                decimal ema = (value - _emas.Last()) * _multiplier + _emas.Last();
                _emas.Add(ema);
            }
        }
    }
}
