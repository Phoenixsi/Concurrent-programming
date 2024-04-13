using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Prezentation
{
    public class View : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AbstractBallsCollection? BallsCollection { get; set; }
        public Start? Start { get; set; }
        public Stop? Stop { get; set; }
        public double CanvasWidth { get; set; }
        public double CanvasHeight { get; set; }

        public int numberOfBalls;

        private string radius;

        public View()
        {

            BallsCollection = new BallsCollection();
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
                BallsCollection.Dispose();
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
                BallsCollection.Dispose();
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
    }
}
