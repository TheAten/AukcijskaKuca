using AukcijskaKuca.Interfaces;
using System.Windows;

namespace AukcijskaKuca.View
{
    /// <summary>
    /// Interaction logic for CreateNewUserView.xaml
    /// </summary>
    public partial class CreateNewUserView : Window
    {
        public CreateNewUserView()
        {
            InitializeComponent();
            Loaded += CreateNewUserView_Loaded;
        }

        private void CreateNewUserView_Loaded(object sender, RoutedEventArgs e)
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
