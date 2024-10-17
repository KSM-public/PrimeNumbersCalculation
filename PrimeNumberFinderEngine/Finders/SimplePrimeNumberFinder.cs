using System.Diagnostics;

namespace PrimeNumberFinderEngine.Finders
{
    public class SimplePrimeNumberFinder : IPrimeNumberFinder
    {
        private PrimeNumberCycle? _primeNumberCycle;
        private uint _cycleID;
        private ulong _calculatedValue;

        private Stopwatch _numberCalculateTime;
        private Stopwatch _totalCalculationTime;

        public PrimeNumberCycle? PrimeNumberCycle { get { return _primeNumberCycle; } }

        public SimplePrimeNumberFinder(uint cycleID)
        {
            _cycleID = cycleID;

            _numberCalculateTime = new Stopwatch();
            _totalCalculationTime = new Stopwatch();
        }

        public void CalculatePrime(ulong startingValue, CancellationToken token)
        {
            var number = startingValue;
            _calculatedValue = startingValue;

            _totalCalculationTime.Restart();

            while (!token.IsCancellationRequested)
            {
                _numberCalculateTime.Restart();

                if (IsPrimeNumber(number, token))
                {
                    _calculatedValue = number;
                }

                _numberCalculateTime.Stop();

                number++;
            }

            _numberCalculateTime.Stop();
            _totalCalculationTime.Stop();

            _primeNumberCycle = new PrimeNumberCycle(_cycleID, _totalCalculationTime.Elapsed.TotalSeconds, _numberCalculateTime.Elapsed.TotalMilliseconds, _calculatedValue);

            if (_calculatedValue <= startingValue)
            {
                return;
            }

            _cycleID++;
        }

        private bool IsPrimeNumber(ulong number, CancellationToken token)
        {
            if (number <= 1)
            {
                return false;
            }

            if (number == 2)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            var boundary = (ulong)Math.Floor(Math.Sqrt(number));

            for (ulong i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                { 
                    return false; 
                }

                if (token.IsCancellationRequested)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
