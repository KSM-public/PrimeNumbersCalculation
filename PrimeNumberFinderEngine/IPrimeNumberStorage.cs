namespace PrimeNumberFinderEngine
{
    public interface IPrimeNumberStorage
    {
        void InitStorage();
        void SaveCycle(PrimeNumberCycle cycle);
        PrimeNumberCycle GetLastCycle();
    }
}
