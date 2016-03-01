using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        //Constructors
        //=============
        public MainWindow()
        {
            InitializeComponent();
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
        }
    }
}
