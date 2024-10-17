using System.Globalization;
using System.Xml.Linq;

namespace PrimeNumberFinderEngine.Storage
{
    public class PrimeNumberXMLStorage : IPrimeNumberStorage
    {
        string _filePath;

        public PrimeNumberXMLStorage(string filePath)
        {
            _filePath = filePath;
        }

        public void InitStorage()
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                throw new Exception("No XML file path specified.");
            }

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }

            var xdoc = new XDocument(new XElement("PrimeNumbers"));
            xdoc.Save(_filePath);
        }

        public void SaveCycle(PrimeNumberCycle cycle)
        {
            XDocument xDocument = XDocument.Load(_filePath);

            var xmlRoot = xDocument.Root;

            if (xmlRoot == null)
            {
                throw new Exception("Invalid XML file.");
            }

            xmlRoot.Add(SerializePrimeNumberCycle(cycle));

            xDocument.Save(_filePath);
        }

        public PrimeNumberCycle GetLastCycle()
        {
            XDocument xDocument;

            try
            {
                xDocument = XDocument.Load(_filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load XML document.", ex);
            }

            var lastElement = xDocument.Root?.Elements("Cycle").LastOrDefault();

            if (lastElement != null)
            {
                try
                {
                    uint cycleID = ParseElementValue<uint>(lastElement, "CycleID");
                    double totalCycleTime = ParseElementValue<double>(lastElement, "TotalCycleTime");
                    double numberCalculationTime = ParseElementValue<double>(lastElement, "NumberCalculationTime");
                    ulong calculatedValue = ParseElementValue<ulong>(lastElement, "CalculatedValue");

                    return new PrimeNumberCycle(cycleID, totalCycleTime, numberCalculationTime, calculatedValue);
                }
                catch (FormatException ex)
                {
                    throw new InvalidOperationException("Failed to parse cycle data.", ex);
                }
            }

            return new PrimeNumberCycle(0, 0, 0, 0);
        }

        private T ParseElementValue<T>(XElement element, string elementName)
        {
            var valueElement = element.Element(elementName);
            if (valueElement == null || string.IsNullOrWhiteSpace(valueElement.Value))
            {
                throw new InvalidOperationException($"Element '{elementName}' is missing or empty.");
            }

            return (T)Convert.ChangeType(valueElement.Value, typeof(T), CultureInfo.InvariantCulture);
        }

        private XElement SerializePrimeNumberCycle(PrimeNumberCycle cycle)
        {
            var element = new XElement("Cycle");
            element.Add(new XElement("CycleID", cycle.CycleID));
            element.Add(new XElement("TotalCycleTime", cycle.TotalCycleTime));
            element.Add(new XElement("NumberCalculationTime", cycle.NumberCalculationTime));
            element.Add(new XElement("CalculatedValue", cycle.CalculatedValue));

            return element;
        }
    }
}
