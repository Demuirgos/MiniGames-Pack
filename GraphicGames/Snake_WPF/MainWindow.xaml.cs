using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private game innerGame;
        DispatcherTimer innerClock;
        void initNewGame()
        {
            this.innerGame = new game(innerClock, this.gameHolder, (int)this.gameHolder.Height, (int)this.gameHolder.Width);
            innerGame.gameEnded += gameEnded;
        }
        public MainWindow()
        {
            InitializeComponent();
            initNewGame();
            this.ResizeMode = ResizeMode.NoResize;
            this.innerClock = new DispatcherTimer();
            innerClock.Interval = TimeSpan.FromMilliseconds(50);
            innerClock.Tick += timer_Tick;
            innerClock.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            innerGame.tickTock();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.innerGame.Start();
            this.Start.Visibility = Visibility.Collapsed;
            this.Pause.Visibility = Visibility.Visible;
        }

        public void gameEnded()
        {
            this.innerClock.Stop();
            this.gameHolder.Children.Clear();
            RestartGame();
        }

        private void RestartGame()
        {
            this.gameHolder.Children.Clear();
            this.Start.Visibility = Visibility.Visible;
            this.Pause.Visibility = Visibility.Collapsed;
            innerClock.Stop();
            initNewGame();
            innerClock.Start();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            this.innerGame.Pause();
            this.Start.Visibility = Visibility.Visible;
            this.Pause.Visibility = Visibility.Collapsed;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            innerClock.Stop();
            System.Windows.Application.Current.Shutdown();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }
    }
}
