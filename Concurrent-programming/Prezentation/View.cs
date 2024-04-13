using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Prezentation
{
    public class View : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AbstractBallsCollection BallsCollection { get; set; }
        public Start Start { get; set; }
        public Stop Stop { get; set; }
        private double canvasWidth;
        private double canvasHeight;

        public int numberOfBalls;

        private string radius;

        public View()
        {

            BallsCollection = new BallsCollection(871, 478.04);

            Start = new Start(this);
            Stop = new Stop(this);


            numberOfBalls = 0;
            radius = "50";
        }

        public string Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
                OnPropertyChanged();
                OnSizeOfBallsChange();
            }
        }

        private void OnSizeOfBallsChange()
        {
            if (Stop.CanExecute(null))
            {
                Stop.Execute(null);
                BallsCollection.Clear();
                BallsCollection.ChangeRadius(double.Parse(Radius));
                BallsCollection.InitBalls(numberOfBalls);
            }
        }

        public int NumberOfBalls
        {
            get => numberOfBalls;
            set
            {
                numberOfBalls = value;
                OnPropertyChanged(nameof(NumberOfBalls));
                OnNumberOfBallsChange();
                Start.PokePossibleExecuteChanged();
            }
        }

        private void OnNumberOfBallsChange()
        {
            if (Stop.CanExecute(null))
            {
                Stop.Execute(null);
                BallsCollection.Clear();
                BallsCollection.InitBalls(numberOfBalls);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        
        internal void OnStartCommand()
        {
            BallsCollection.StartTimer();
        }

        internal void OnStopCommand()
        {
            BallsCollection.StopTimer();
        }

        public double CanvasWidth
        {
            get { return canvasWidth; }
            set
            {
                if (canvasWidth != value)
                {
                    canvasWidth = value;
                    OnPropertyChanged("CanvasWidth");
                }
            }
        }

        public double CanvasHeight
        {
            get { return canvasHeight; }
            set
            {
                if (canvasHeight != value)
                {
                    canvasHeight = value;
                    OnPropertyChanged("CanvasHeight");
                }
            }
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if (canvas != null)
            {
                CanvasWidth = canvas.ActualWidth;
                CanvasHeight = canvas.ActualHeight;
                BallsCollection = new BallsCollection(CanvasWidth, CanvasHeight);
            }
        }


    }
}
