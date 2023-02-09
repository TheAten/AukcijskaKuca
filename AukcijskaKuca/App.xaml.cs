using AukcijskaKuca.Model;
using AukcijskaKuca.Stores;
using AukcijskaKuca.View;
using AukcijskaKuca.ViewModel;
using System.Windows;

namespace AukcijskaKuca
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        //private Countdown? countdown;
        //private TimerStore? _timerStore;
        //private CountdownViewModel? _countdownViewModel;

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    _timerStore = new TimerStore();
        //    _countdownViewModel = new CountdownViewModel(_timerStore);

        //    countdown = new Countdown
        //    {
        //        DataContext = _countdownViewModel
        //    };
        //    countdown.Show();

        //    base.OnStartup(e);
        //}


        private LogInView? _loginView;
        private TimerStore? _timerStore;
        private LogInViewModel? _loginViewModel;

        private AuctionView _auctionView;
        private AuctionViewModel _auctionViewModel;
        private Users _users;

        protected override void OnStartup(StartupEventArgs e)
        {
            _timerStore = new TimerStore();
            _users= new Users();
            
            _loginViewModel = new LogInViewModel();
            _auctionViewModel = new AuctionViewModel(_timerStore, _users);


            //_loginView = new LogInView
            //{
            //    DataContext = _loginViewModel
            //};
            //_loginView.Show();

            _auctionView = new AuctionView
            {
                DataContext = _auctionViewModel
            };
            _auctionView.Show();

            base.OnStartup(e);
        }
    }
}
