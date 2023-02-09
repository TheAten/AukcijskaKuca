using AukcijskaKuca.Commands;
using AukcijskaKuca.Data_Access;
using AukcijskaKuca.Interfaces;
using AukcijskaKuca.Model;
using System;
using System.Windows.Input;

namespace AukcijskaKuca.ViewModel
{
    public class CreateNewUserViewModel : ICloseWindow
    {

        public Action CloseWindow { get; set; }

        private Users newUser;

        public Users NewUser
        {
            get { return newUser; }
            set { newUser = value; }
        }

        private ICommand createUser;

        public ICommand CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }



        void CreateExecute(object obj)
        {
            int i = DBConfiguration.connection.RegisterCheck(NewUser);
            if (i == 0 &&
                NewUser.Username != null && NewUser.Password != null)
            {
                DBConfiguration.connection.CreateUser(NewUser);
                CloseWindow?.Invoke();
            }
        }

        bool CanCreate(object obj)
        {
            return true;
        }

        public CreateNewUserViewModel()
        {
            DBConfiguration.InitializeConnection();
            CreateUser = new RelayCommand(CreateExecute, CanCreate);
            NewUser = new Users();
        }
    }
}
