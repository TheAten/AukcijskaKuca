using AukcijskaKuca.Stores;

using AukcijskaKuca.ViewModel;

namespace AukcijskaKuca.Commands
{
    public class StartCommand : BaseCommand
    {
        private readonly AuctionViewModel _viewModel;
        private readonly TimerStore _timerStore;

        public StartCommand(AuctionViewModel viewModel, TimerStore timerStore)
        {
            _viewModel = viewModel;
            _timerStore = timerStore;
        }

        public override void Execute(object? parameter)
        {
            _timerStore.Start(_viewModel.Duration);

        }


        //private readonly CountdownViewModel _viewModel;
        //private readonly TimerStore _timerStore;

        //public StartCommand(CountdownViewModel viewModel, TimerStore timerStore)
        //{
        //    _viewModel = viewModel;
        //    _timerStore = timerStore;
        //}

        //public override void Execute(object? parameter)
        //{
        //    _timerStore.Start(_viewModel.Duration);
        //}        
    }
}
