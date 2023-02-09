using AukcijskaKuca.Stores;
using System.Windows.Input;

namespace AukcijskaKuca.ViewModel
{
    public class CountdownViewModel : ViewModelBase
    {
        private readonly TimerStore _timerStore;

        private int duration;

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
        public double RemainingSeconds => _timerStore.RemainingSeconds;

        public ICommand StartCommand { get; }


        public CountdownViewModel(TimerStore timerStore)
        {
            Duration = 12;

            _timerStore = timerStore;

            // StartCommand = new StartCommand(this, _timerStore);

            _timerStore.RemainingSecondsChanged += TimerStore_RemainingSecondsChanged;
        }

        private void TimerStore_RemainingSecondsChanged()
        {
            OnPropertyChanged(nameof(RemainingSeconds));
        }
    }
}
