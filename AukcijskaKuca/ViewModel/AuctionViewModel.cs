using AukcijskaKuca.Commands;
using AukcijskaKuca.Data_Access;
using AukcijskaKuca.Interfaces;
using AukcijskaKuca.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AukcijskaKuca.ViewModel
{
	public class AuctionViewModel : IOpenWindow, INotifyPropertyChanged
	{

        //TODO popraviti ovo glupo dinamicko loadanje listboxa
        public Action StartTimer { get; set; }

        private Product newProduct;

        public Product NewProduct
        {
            get { return newProduct; }
            set { newProduct = value; }
        }

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }

        void SaveExecute(object obj)
        {
            DBConfiguration.connection.CreateProduct(NewProduct);
            MessageBox.Show("Created");
			StartTimer?.Invoke();
        }

		bool CanSaveExexute(object obj)
        {
            if (NewProduct.Name != null && NewProduct.Price != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Product lastProduct;

		public Product LastProduct
		{
			get { return lastProduct; }
			set { lastProduct = value; }
		}


		public event PropertyChangedEventHandler? PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var list = PropertyChanged;
			if (list != null) list(this, e);
		}
		public Action OpenNew { get; set; }

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
				OnPropertyChanged(new PropertyChangedEventArgs("ProductCollection"));
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


		void NewExecute(object obj)
		{
			throw new NotImplementedException();
		}

		void DeleteExecute(object obj)
		{
			if (CurrentProduct != null)
			{
				DBConfiguration.connection.DeleteProduct(CurrentProduct.Id);
				MessageBox.Show("User Deleted");
			}
		}

		bool CanExecute(object obj)
		{
			//TODO - Dodati funkcionalnost u kooj samo admin moze da pritisne button
			return true;
		}

		bool CanDeleteExecute(object obj)
		{
			return true;
		}


		public AuctionViewModel()
		{
			DBConfiguration.InitializeConnection();
			NewProductView = new RelayCommand(NewExecute, CanExecute);
			DeleteProduct = new RelayCommand(DeleteExecute, CanDeleteExecute);
			ProductList = DBConfiguration.connection.GetAllProducts();
			ProductCollection = new ObservableCollection<Product>(ProductList);
			LastProduct = ProductCollection.Last();
            SaveCommand = new RelayCommand(SaveExecute, CanSaveExexute);
            NewProduct = new Product();
        }
	}
}
