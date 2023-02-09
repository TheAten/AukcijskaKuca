using System;
using System.Diagnostics;
using System.Timers;

namespace AukcijskaKuca.Stores
{
    public class TimerStore
    {
        /// <summary>
        /// Class for defining timer and its settings
        /// </summary>


        private readonly Timer _timer;

        private DateTime _endTime;

        /// <summary>
        /// Izracunava koliko je sekundi ostalo tako sto oduzima dato vrijeme od trenutnog
        /// </summary>
        public double EndTimeCurrentTimeSecondsDifference => TimeSpan.FromTicks(_endTime.Ticks).TotalSeconds -
            TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;



        public double RemainingSeconds => EndTimeCurrentTimeSecondsDifference > 0 ? EndTimeCurrentTimeSecondsDifference : 0;



        public event Action? RemainingSecondsChanged;

        public TimerStore()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
        }

        public void Start(int durationInSeconds)
        {
            _timer.Start();
            _endTime = DateTime.Now.AddSeconds(durationInSeconds);
            OnRemainingSecondsChanged();
        }


        public void Stop()
        {
            if (RemainingSeconds == 0)
            {
                Trace.WriteLine("zero");
                _timer.Stop();

            }

        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Stop();

            OnRemainingSecondsChanged();
        }

        private void OnRemainingSecondsChanged()
        {

            RemainingSecondsChanged?.Invoke();
        }
    }
}
