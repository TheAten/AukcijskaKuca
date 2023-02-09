using AukcijskaKuca.Interfaces;
using AukcijskaKuca.ViewModel;
using System.Windows;

namespace AukcijskaKuca.View
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        public LogInView()
        {
            InitializeComponent();

            Loaded += LogInView_Loaded;
        }

        private void LogInView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindow vm)
            {
                vm.CloseWindow += () =>
                {
                    this.Close();
                };
            }

            if (DataContext is IOpenWinow ow)
            {

                ow.OpenNew += () =>
                {
                    CreateNewUserView create = new CreateNewUserView
                    {
                        DataContext = new CreateNewUserViewModel()
                    };
                    create.ShowDialog();
                };

            }
        }
    }
}
