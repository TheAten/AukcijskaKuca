using AukcijskaKuca.Commands;
using AukcijskaKuca.Data_Access;
using AukcijskaKuca.Helpers;
using AukcijskaKuca.Interfaces;
using AukcijskaKuca.Model;
using AukcijskaKuca.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AukcijskaKuca.ViewModel
{
    public class AuctionViewModel : PropertyChangedBase,  ITimer, IOpenWinow
    {
        //TODO popraviti ovo glupo dinamicko loadanje listboxa
        private int i;

        #region timer stufff

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

        private void TimerStore_RemainingSecondsChanged()
        {
            OnPropertyChanged(nameof(RemainingSeconds));
        }

        #endregion

        private Users currentUser;

        public Users CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }


        public Action StartTimer { get; set; }

        public Action OpenNew { get; set; }

        private Product newProduct;

        public Product NewProduct
        {
            get { return newProduct; }
            set
            {
                newProduct = value;
                OnPropertyChanged(nameof(NewProduct));
            }
        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }

        private Product lastProduct;

        public Product LastProduct
        {
            get { return lastProduct; }
            set { lastProduct = value; }
        }

        private Product currentProduct;

        public Product CurrentProduct
        {
            get { return currentProduct; }
            set { currentProduct = value; }
        }

        private List<Product> productList;

        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; }
        }

        private ObservableCollection<Product> productCollection;

        public ObservableCollection<Product> ProductCollection
        {
            get { return productCollection; }
            set
            {
                if (productCollection == value) return;
                productCollection = value;

            }
        }

        private ICommand newProductView;

        public ICommand NewProductView
        {
            get { return newProductView; }
            set { newProductView = value; }
        }

        private ICommand deleteProduct;

        public ICommand DeleteProduct
        {
            get { return deleteProduct; }
            set { deleteProduct = value; }
        }

        private ICommand bidCommand;

        public ICommand BidCommand
        {

            get { return bidCommand; }
            set { bidCommand = value; }
        }

        private ICommand startCommand;

        public ICommand StartCommand
        {
            get { return startCommand; }
            set { startCommand = value; }
        }

        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get { return loginCommand; }
            set { loginCommand = value; }
        }

        void LoginExecute(object obj)
        {
            OpenNew?.Invoke();
        }

        bool CanLogin(object obj)
        {
            return true;
        }

        void StartExecute(object obj)
        {
            DBConfiguration.connection.CreateProduct(NewProduct);
            MessageBox.Show("Created");
            _timerStore.Start(Duration);
        }

        bool CanStartExexute(object obj)
        {
            i = DBConfiguration.connection.AdminAccess(CurrentUser);

            if (NewProduct.Name != null && NewProduct.Price != 0 && i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void DeleteExecute(object obj)
        {
            if (CurrentProduct != null)
            {
                DBConfiguration.connection.DeleteProduct(CurrentProduct.Id);
                MessageBox.Show("User Deleted");
            }
        }

        bool CanDeleteExecute(object obj)
        {
            if (i == 1) { return true; }
            else return false;
        }

        void BidExecute(object obj)
        {
            Trace.WriteLine(NewProduct.Price.ToString());
            NewProduct.Price++;
            Trace.WriteLine(NewProduct.Price.ToString());
            Trace.WriteLine(CurrentUser.Username);
            Trace.WriteLine(_timerStore.RemainingSeconds.ToString());
            DBConfiguration.connection.UpdateProduct(NewProduct, NewProduct.Id);

            _timerStore.Start(10);

            // nakon pritiskanja netko button vrati se id korisnika 
            // _timer.ID = IDBConnectrion 

        }

        bool BidCanExecute(object obj)
        {
            if ((i == 1 || i == 2) && _timerStore.RemainingSeconds > 0)
            {
                return true;
            }
            else return false;
        }


        public AuctionViewModel(TimerStore timerStore, Users model)
        {
            DBConfiguration.InitializeConnection();

            CurrentUser = model;
            

            Duration = 5;
            _timerStore = timerStore;
            _timerStore.RemainingSecondsChanged += TimerStore_RemainingSecondsChanged;

            StartCommand = new RelayCommand(StartExecute, CanStartExexute);
            LoginCommand = new RelayCommand(LoginExecute, CanLogin);
            DeleteProduct = new RelayCommand(DeleteExecute, CanDeleteExecute);
            BidCommand = new RelayCommand(BidExecute, BidCanExecute);

            ProductList = DBConfiguration.connection.GetAllProducts();
            ProductCollection = new ObservableCollection<Product>(ProductList);
            LastProduct = ProductCollection.Last();

            NewProduct = new Product();
        }
    }

    // Admin ima svako pravo\
    // Korisnik ima pravo samo na Bid command
    // Gost nema pravo na nista 
}
