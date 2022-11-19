using AukcijskaKuca.Interfaces;
using AukcijskaKuca.ViewModel;
using System.Windows;
using System.Windows.Threading;

namespace AukcijskaKuca.View
{
    /// <summary>
    /// Interaction logic for AuctionView.xaml
    /// </summary>
    public partial class AuctionView : Window
    {

        private int time;
        private DispatcherTimer Timer;
        public AuctionView()
        {
            InitializeComponent();
           
            Loaded += AuctionView_Loaded;

        }

        private void AuctionView_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is IOpenWindow vm)
            {
                vm.StartTimer += () =>
                {
                    time = 5;
                    Timer = new DispatcherTimer();
                    Timer.Interval = new System.TimeSpan(0, 0, 1);
                    Timer.Tick += Timer_Tick;
                    Timer.Start();
                };
            }
        }

        private void Timer_Tick(object? sender, System.EventArgs e)
        {
            if (time > 0)
            {
                time--;
                timerTextBox.Text = string.Format("00:0{0}:{1}",time/60,time%60);

            }
            else
            {
                Timer.Stop();
                MessageBox.Show("Timer stoped");
            }
           
        }
    }
}
