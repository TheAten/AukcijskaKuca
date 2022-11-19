using AukcijskaKuca.Commands;
using AukcijskaKuca.Data_Access;
using AukcijskaKuca.Interfaces;
using AukcijskaKuca.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace AukcijskaKuca.ViewModel
{
    public class AuctionViewModel : IOpenWindow, INotifyPropertyChanged
    {

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
			set {
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

		void NewExecute(object obj)
		{
			OpenNew?.Invoke();
		}

		bool CanExecute(object obj)
		{
			return true;
		}


        public AuctionViewModel()
		{
			DBConfiguration.InitializeConnection();
			NewProductView = new RelayCommand(NewExecute, CanExecute);
            ProductList = DBConfiguration.connection.GetAllProducts();
			ProductCollection = new ObservableCollection<Product>(ProductList);
			
		}
	}
}
