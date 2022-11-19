using AukcijskaKuca.Interfaces;
using System.Windows;

namespace AukcijskaKuca.View
{
    /// <summary>
    /// Interaction logic for NewProductView.xaml
    /// </summary>
    public partial class NewProductView : Window
    {
        public NewProductView()
        {
            InitializeComponent();
            Loaded += NewProductView_Loaded;
        }

        private void NewProductView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindow vm)
            {
                vm.CloseWindow += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
