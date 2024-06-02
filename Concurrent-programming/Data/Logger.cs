using Data;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Data
{
    public class Logger
    {
        private Timer _timer;
        private ObservableCollection<AbstractBall> _balls;
        private string _logFilePath;

        public Logger(ObservableCollection<AbstractBall> balls, string logFilePath)
        {
            _balls = balls;
            _logFilePath = logFilePath;
            InitializeTimer();
        }

        public void InitializeTimer()
        {
            _timer = new Timer(3000); // Wywołaj LogEvent co 1000 milisekund (1 sekunda)
            _timer.Elapsed += LogEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void LogEvent(Object source, ElapsedEventArgs e)
        {
            lock(this)
            {
                using (StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + _logFilePath, true))
                {
                    string logMessage = "Log from " + e.SignalTime + "\n";
                    Debug.WriteLine(logMessage);
                    foreach (Ball ball in _balls)
                    {
                        string ballData = JsonSerializer.Serialize(ball);
                        streamWriter.WriteLine(logMessage + ballData);

                    }
                }
            }
        }

        public void StopLogging()
        {
            _timer.Stop();
        }
    }

}