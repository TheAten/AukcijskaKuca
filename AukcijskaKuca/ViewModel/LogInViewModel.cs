using AukcijskaKuca.Commands;
using AukcijskaKuca.Data_Access;
using AukcijskaKuca.Interfaces;
using AukcijskaKuca.Model;
using AukcijskaKuca.Stores;
using AukcijskaKuca.View;
using System;
using System.Windows.Input;

namespace AukcijskaKuca.ViewModel
{
    public class LogInViewModel : ICloseWindow, IOpenWinow

    {
        private TimerStore _timerStore;
        public Action CloseWindow { get; set; }
        public Action OpenNew { get; set; }

        private Users users;

        public Users Users
        {
            get { return users; }
            set { users = value; }
        }

        private ICommand loginCommandFirst;

        public ICommand LoginCommandFirst
        {
            get { return loginCommandFirst; }
            set { loginCommandFirst = value; }
        }

        private ICommand createCommand;

        public ICommand CreateCommand
        {
            get { return createCommand; }
            set { createCommand = value; }
        }

        void CreateExecute(object obj)
        {
            OpenNew?.Invoke();
        }

        bool CanCreate(object obj)
        {
            return true;
        }

        void LoginExecute(object obj)
        {
            if (DBConfiguration.connection.LoginCheck(Users) == 1)
            {
                _timerStore = new TimerStore();
                //AuctionView auctionView = new AuctionView();
                //auctionView.DataContext = new AuctionViewModel(_timerStore);
                //auctionView.Show();
                AuctionViewModel auctionViewModel = new AuctionViewModel(_timerStore, Users);
                AuctionView auctionView = new()
                {
                    DataContext = auctionViewModel
                };
                auctionView.Show();
                CloseWindow?.Invoke();
            }
        }

        bool CanLogin(object obj)
        {
            return true;
        }

        public LogInViewModel()
        {
            Users = new Users();
            DBConfiguration.InitializeConnection();
            LoginCommandFirst = new RelayCommand(LoginExecute, CanLogin);
            CreateCommand = new RelayCommand(CreateExecute, CanCreate);
        }
    }
}
