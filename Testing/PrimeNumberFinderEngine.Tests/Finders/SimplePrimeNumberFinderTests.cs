using PrimeNumberFinderEngine.Finders;
using PrimeNumberFinderEngine.Tests.Utils;

namespace PrimeNumberFinderEngine.Tests.Finders
{
    public class SimplePrimeNumberFinderTests
    {
        static PrimeNumberCycle? RunSimplePrimeNumberFinder(SimplePrimeNumberFinder simplePrimeNumberFinder, ulong startingValue, int interval)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var calculatePrimeTask = new Task(() => simplePrimeNumberFinder.CalculatePrime(startingValue, cancellationTokenSource.Token));
            cancellationTokenSource.CancelAfter(interval);
            calculatePrimeTask.Start();

            calculatePrimeTask.Wait();
            return simplePrimeNumberFinder.PrimeNumberCycle;
        }

        [Test]
        public void PrimeNumbersCyclesAreCalculatedCorrectly()
        {
            var primeNumberCycles = new List<PrimeNumberCycle>();
            var simplePrimeNumberFinder = new SimplePrimeNumberFinder(0);

            var primeNumberCycle = RunSimplePrimeNumberFinder(simplePrimeNumberFinder, 2, 10);

            if (primeNumberCycle != null)
            {
                primeNumberCycles.Add(primeNumberCycle);
            }

            Assert.That(primeNumberCycles.Count, Is.EqualTo(1));

            primeNumberCycle = RunSimplePrimeNumberFinder(simplePrimeNumberFinder, primeNumberCycles.Last().CalculatedValue, 10);

            if (primeNumberCycle != null)
            {
                primeNumberCycles.Add(primeNumberCycle);
            }

            Assert.That(primeNumberCycles.Count, Is.EqualTo(2));

            primeNumberCycle = RunSimplePrimeNumberFinder(simplePrimeNumberFinder, primeNumberCycles.Last().CalculatedValue, 10);

            if (primeNumberCycle != null)
            {
                primeNumberCycles.Add(primeNumberCycle);
            }

            Assert.That(primeNumberCycles.Count, Is.EqualTo(3));

            Assert.That(primeNumberCycles[0].CycleID, Is.EqualTo(0));
            Assert.That(PrimeNumberChecker.IsPrimeNumber(primeNumberCycles[0].CalculatedValue),  Is.True);

            Assert.That(primeNumberCycles[1].CycleID, Is.EqualTo(1));
            Assert.That(PrimeNumberChecker.IsPrimeNumber(primeNumberCycles[1].CalculatedValue), Is.True);
            Assert.That(primeNumberCycles[1].CalculatedValue, Is.GreaterThan(primeNumberCycles[0].CalculatedValue));

            Assert.That(primeNumberCycles[2].CycleID, Is.EqualTo(2));
            Assert.That(PrimeNumberChecker.IsPrimeNumber(primeNumberCycles[2].CalculatedValue), Is.True);
            Assert.That(primeNumberCycles[2].CalculatedValue, Is.GreaterThan(primeNumberCycles[1].CalculatedValue));
        }
    }
}
