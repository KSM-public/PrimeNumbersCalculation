using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberFinderEngine
{
    public class PrimeNumber
    {
        public ulong Number { get; set; }
        public long CalculateTime { get; set; }

        public PrimeNumber()
        {
            Number = 0;
            CalculateTime = 0;
        }

        public PrimeNumber(ulong number, long calculateTime)
        {
            Number = number;
            CalculateTime = calculateTime;
        }
    }
}
