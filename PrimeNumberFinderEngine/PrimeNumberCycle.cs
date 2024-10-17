namespace PrimeNumberFinderEngine
{
    public class PrimeNumberCycle
    {
        public uint CycleID { get; }
        public double TotalCycleTime { get; }
        public double NumberCalculationTime { get; }
        public ulong CalculatedValue { get; }

        public PrimeNumberCycle()
        {
            CycleID = 0;
            TotalCycleTime = 0;
            NumberCalculationTime = 0;
            CalculatedValue = 0;
        }

        public PrimeNumberCycle(uint cycleID, double totalCycleTime, double numberCalculationTime, ulong calculatedValue)
        {
            CycleID = cycleID;
            TotalCycleTime = totalCycleTime;
            NumberCalculationTime = numberCalculationTime;
            CalculatedValue = calculatedValue;
        }
    }
}
