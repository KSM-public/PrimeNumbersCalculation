namespace PrimeNumberFinderEngine
{
    public interface IPrimeNumberFinder
    {
        PrimeNumberCycle? PrimeNumberCycle { get; }
        void CalculatePrime(ulong startingValue, CancellationToken token);
    }
}
