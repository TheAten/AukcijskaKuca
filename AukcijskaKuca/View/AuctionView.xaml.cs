using AukcijskaKuca.Interfaces;
using AukcijskaKuca.ViewModel;
using System.Windows;

namespace AukcijskaKuca.View
{
    /// <summary>
    /// Interaction logic for AuctionView.xaml
    /// </summary>
    public partial class AuctionView : Window
    {
        public AuctionView()
        {
            InitializeComponent();
            Loaded += AuctionView_Loaded;
        }

        private void AuctionView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IOpenWinow vm)
            {
                vm.OpenNew += () =>
                {
                    LogInView logInView = new()
                    {
                        DataContext = new LogInViewModel()
                    };
                };
            }
        }
    }
}
