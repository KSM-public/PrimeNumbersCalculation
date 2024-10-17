using PrimeNumberFinderEngine;
using PrimeNumberFinderEngine.Finders;
using PrimeNumberFinderEngine.Storage;
using System.Diagnostics;

namespace PrimeNumberFinderGUI
{
    public partial class Form1 : Form
    {
        const int CalculationInterval = 2 * 60 * 1000; //2 minutes
        const int WaitingInterval = 1 * 60 * 1000; //1 minute

        private CancellationTokenSource? _cancellationTokenSource;
        private IPrimeNumberFinder? _primeNumberFinder;
        private IPrimeNumberStorage? _primeNumberStorage;
        private Task? _calculatePrimeTask;
        private uint _cycleID;
        private ulong _startValue;
        private ProgramState _programState;
        private double _remainingTime;
        private Stopwatch _remainingTimeStopwatch;

        public Form1()
        {
            InitializeComponent();

            _cycleID = 0;
            _startValue = 2;
            _programState = ProgramState.Ready;
            _remainingTime = 0;
            _remainingTimeStopwatch = new Stopwatch();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_programState == ProgramState.Ready)
            {
                if (_primeNumberStorage == null)
                {
                    try
                    {
                        InitStorage();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Storage initialization failed: " + ex.Message);
                    }
                }

                if (_primeNumberStorage == null)
                {
                    return;
                }

                try
                {
                    BeginCalculation();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }

                _remainingTime = CalculationInterval;
                StartTimers(CalculationInterval);
                UpdateTimer();

                _programState = ProgramState.Calculating;
                UpdateStatus();
            }
            else
            {
                StopTimers();

                if (_programState == ProgramState.Calculating)
                {
                    try
                    {
                        StopCalculation();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

                _programState = ProgramState.Ready;
                UpdateStatus();
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            //Stop calculation and begin waiting
            if (_programState == ProgramState.Calculating)
            {
                StopTimers();

                try
                {
                    StopCalculation();
                }
                catch (Exception ex) //Stop operation on error
                {
                    _programState = ProgramState.Ready;
                    UpdateStatus();

                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }

                _remainingTime = WaitingInterval;
                StartTimers(WaitingInterval);
                UpdateTimer();

                _programState = ProgramState.Waiting;
                UpdateStatus();
            }
            else if (_programState == ProgramState.Waiting) //Begin calculation
            {
                try
                {
                    BeginCalculation();
                }
                catch (Exception ex) //Stop operation on error
                {
                    _programState = ProgramState.Ready;
                    UpdateStatus();

                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }

                _cycleID++;

                _remainingTime = CalculationInterval;
                StartTimers(CalculationInterval);
                UpdateTimer();

                _programState = ProgramState.Calculating;
                UpdateStatus();
            }
        }

        private void tmrElapsed_Tick(object sender, EventArgs e)
        {
            _remainingTime -= _remainingTimeStopwatch.ElapsedMilliseconds;
            _remainingTimeStopwatch.Restart();

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            var timeSpan = TimeSpan.FromMilliseconds(_remainingTime);

            var elapsedTimeString = string.Format("{0:D2}:{1:D2}",
                    timeSpan.Minutes,
                    timeSpan.Seconds);

            lblElapsedTime.Text = elapsedTimeString;
        }

        private void UpdateStatus()
        {
            switch (_programState)
            {
                case ProgramState.Ready:
                    btnLoadData.Enabled = true;
                    btnStartStop.Text = "Start";
                    lblStatus.Text = "Ready";
                    pbrCalculationProgress.Style = ProgressBarStyle.Blocks;
                    break;

                case ProgramState.Waiting:
                    btnLoadData.Enabled = false;
                    btnStartStop.Text = "Stop";
                    lblStatus.Text = "Waiting";
                    pbrCalculationProgress.Style = ProgressBarStyle.Marquee;
                    break;

                case ProgramState.Calculating:
                    btnLoadData.Enabled = false;
                    btnStartStop.Text = "Stop";
                    lblStatus.Text = "Calculating cycle " + _cycleID.ToString();
                    pbrCalculationProgress.Style = ProgressBarStyle.Marquee;
                    break;
            }
        }

        private void StartTimers(int interval)
        {
            tmrMain.Interval = interval;
            tmrMain.Start();

            _remainingTime = interval;
            _remainingTimeStopwatch.Restart();
            tmrRemaining.Start();
        }

        private void StopTimers()
        {
            tmrMain.Stop();
            tmrRemaining.Stop();
        }

        private void DisplayCycleData(PrimeNumberCycle cycle)
        {
            lblCycleID.Text = cycle.CycleID.ToString();
            lblTotalCycleTime.Text = cycle.TotalCycleTime.ToString("F2") + "s";
            lblNumberCalculationTime.Text = cycle.NumberCalculationTime.ToString() + "ms";
            lblCalculatedNumber.Text = cycle.CalculatedValue.ToString();
        }

        private void InitStorage()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File|*.xml";
            saveFileDialog.Title = "Save calculation to XML";

            var result = saveFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            var xmlStorage = new PrimeNumberXMLStorage(saveFileDialog.FileName);

            try
            {
                xmlStorage.InitStorage();
            }
            catch (Exception ex)
            {
                throw new Exception("XML Storage: " + ex.Message);
            }

            _primeNumberStorage = xmlStorage;
        }

        private void BeginCalculation()
        {
            if (_programState == ProgramState.Calculating)
            {
                throw new Exception("Operation already pending.");
            }

            _cancellationTokenSource = new CancellationTokenSource();

            if (_primeNumberFinder == null)
            {
                _primeNumberFinder = new SimplePrimeNumberFinder(_cycleID);
            }

            _calculatePrimeTask = new Task(() => _primeNumberFinder.CalculatePrime(_startValue, _cancellationTokenSource.Token));
            _calculatePrimeTask.Start();
        }

        private void StopCalculation()
        {
            if (_programState != ProgramState.Calculating)
            {
                throw new Exception("Operation cannot be canceled.");
            }

            if (_cancellationTokenSource == null || _calculatePrimeTask == null || _primeNumberFinder == null
                || _primeNumberStorage == null)
            {
                throw new NullReferenceException("One of the required objects is null.");
            }

            _cancellationTokenSource.Cancel();

            try
            {
                _calculatePrimeTask.Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Calculation failed: " + ex.Message);
            }

            if (_primeNumberFinder.PrimeNumberCycle == null)
            {
                throw new Exception("Calculation result is null.");
            }

            if (_primeNumberFinder.PrimeNumberCycle.CalculatedValue <= _startValue)
            {
                throw new Exception("Calculated value is not greater than start value.");
            }

            try
            {
                _primeNumberStorage.SaveCycle(_primeNumberFinder.PrimeNumberCycle);
            }
            catch (Exception ex)
            {
                _primeNumberStorage = null;
                throw new Exception("Saving cycle failed: " + ex.Message);
            }

            _startValue = _primeNumberFinder.PrimeNumberCycle.CalculatedValue;

            DisplayCycleData(_primeNumberFinder.PrimeNumberCycle);
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            if (_programState != ProgramState.Ready)
            {
                return;
            }

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML File|*.xml";
            openFileDialog.Title = "Load data from XML";
            var result = openFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            _primeNumberStorage = new PrimeNumberXMLStorage(openFileDialog.FileName);

            try
            {
                var lastCycle = _primeNumberStorage.GetLastCycle();
                DisplayCycleData(lastCycle);

                _cycleID = lastCycle.CycleID + 1;
                _startValue = lastCycle.CalculatedValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Storage initialization failed: " + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save calculated data before close
            if (_programState == ProgramState.Calculating)
            {
                try
                {
                    StopCalculation();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
