using AukcijskaKuca.Commands;
using AukcijskaKuca.Data_Access;
using AukcijskaKuca.Interfaces;
using AukcijskaKuca.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace AukcijskaKuca.ViewModel
{
	public class NewProductViewModel : ICloseWindow
	{
		public Action CloseWindow { get; set; }

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
			CloseWindow?.Invoke();
		}

		bool CanSaveExexute(object obj)
		{
			if(NewProduct.Name != null && NewProduct.Price != 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public NewProductViewModel()
		{
			DBConfiguration.InitializeConnection();
			SaveCommand = new RelayCommand(SaveExecute, CanSaveExexute);
			NewProduct = new Product();
		}
	}
}
