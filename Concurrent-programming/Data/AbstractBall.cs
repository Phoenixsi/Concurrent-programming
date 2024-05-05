// AbstractBall.cs
using System;
using System.ComponentModel;
using System.Numerics;

namespace Data
{
    /// <summary>
    /// Abstract base class representing a ball.
    /// </summary>
    public abstract class AbstractBall : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;

        public abstract int BallID { get; }
        public abstract Vector2 BallPosition { get; set; }
        public abstract Vector2 BallVelocity { get; set; }
        public abstract double BallRadius { get; set; }
        public abstract double BallMass { get; set; }

        public abstract double BallPositionX { get; set; }
        public abstract double BallPositionY { get; set; }

       /* protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
