using Logic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public View(AbstractBallsCollection ballsCol)
        {

            BallsCollection = ballsCol;

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
                if (Radius != "" && Double.Parse(Radius) < 1000) 
                {
                    BallsCollection.ChangeRadius(double.Parse(Radius));
                }

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


    }
}
