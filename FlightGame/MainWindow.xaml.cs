using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlightGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields
        //=======
        private bool _planeTaken = false;
        DispatcherTimer _timer = new DispatcherTimer();
        Random _rnd = new Random();
        private MediaPlayer musicPlayer = new MediaPlayer();

        //Constructors
        //=============
        public MainWindow()
        {
            InitializeComponent();

            this.musicPlayer.Open(new Uri("Teamheadkick - Deadpool Rap (Movie Version).mp3", UriKind.RelativeOrAbsolute));
            this.musicPlayer.MediaEnded += delegate
            {
                this.musicPlayer.Position = TimeSpan.Zero;
                this.musicPlayer.Play();
            };
            this.musicPlayer.Play();
        }

        //Methods
        //========
        private void SpawnPlane()
        {
            Obstakel newPlane = new Obstakel();

            double xPos = this._rnd.Next((int)this.Plane.Width, (int) this.PlayArea.ActualWidth);

            Canvas.SetTop(newPlane, -newPlane.Height);
            Canvas.SetLeft(newPlane, xPos);

            this.PlayArea.Children.Add(newPlane);

            double speed = this._rnd.Next(1, 10);

            StartMoveDown(newPlane, newPlane.ActualHeight, TimeSpan.FromSeconds(speed));
        }

        private void StartMoveDown(UIElement element, double height, TimeSpan duration)
        {
            Storyboard story = new Storyboard();
            DoubleAnimation anim = new DoubleAnimation()
            {
                From = 0,
                To = this.PlayArea.ActualHeight + height,
                Duration = duration
            };

            Storyboard.SetTarget(anim, element);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Top)"));

            story.Children.Add(anim);
            story.Begin();
        }

        //Events
        //=======
        private void GrabPlane_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._planeTaken = true;
        }
        private void MovePlane_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_planeTaken)
            {
                return;
            }

            Point mousePos = Mouse.GetPosition(PlayArea);

            double xPos = mousePos.X - (this.Plane.ActualWidth / 2);
            //double yPos = mousePos.Y - (this.Plane.ActualHeight / 2);

            //Canvas.SetTop(this.Plane, yPos);
            Canvas.SetLeft(this.Plane, xPos);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            double startHeight = this.PlayArea.ActualHeight - (this.Plane.ActualHeight + 30);
            double startWidth = (this.PlayArea.ActualWidth / 2) - (this.Plane.ActualWidth / 2);

            Canvas.SetTop(this.Plane, startHeight);
            Canvas.SetLeft(this.Plane, startWidth);

            this._timer.Interval = TimeSpan.FromSeconds(0.2);
            this._timer.Tick += delegate { SpawnPlane(); };
            this._timer.Start();
        }
}
}
