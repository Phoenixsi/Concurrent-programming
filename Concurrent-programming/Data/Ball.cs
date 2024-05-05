// Ball.cs
using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Data
{
    /// <summary>
    /// Concrete class representing a ball.
    /// </summary>
    public class Ball : AbstractBall
    {
        private readonly int ballID;
        private Vector2 ballPosition;
        private Vector2 ballVelocity;
        private double ballRadius;
        private double ballMass;
        public Brush Color { get; set; }

        public Ball(int ballID, Vector2 ballPosition = default, Vector2 ballVelocity = default, double ballRadius = 10.0, double ballMass = 10.0)
        {
            this.ballID = ballID;
            this.ballPosition = ballPosition;
            this.ballVelocity = ballVelocity;
            this.ballRadius = ballRadius;
            this.ballMass = ballMass;
            this.Color = Brushes.Red; // Default color set to Red
        }

        public override int BallID => ballID;

        public override Vector2 BallPosition
        {
            get => ballPosition;
            set
            {
                if (ballPosition != value)
                {
                    ballPosition = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BallPositionX));
                    OnPropertyChanged(nameof(BallPositionY));
                }
            }
        }

        public override double BallPositionX
        {
            get => BallPosition.X;
            set
            {
                BallPosition = new Vector2((float)value, BallPosition.Y);
                OnPropertyChanged(nameof(BallPositionX));
            }
        }

        public override double BallPositionY
        {
            get => BallPosition.Y;
            set
            {
                BallPosition = new Vector2(BallPosition.X, (float)value);
                OnPropertyChanged(nameof(BallPositionY));
            }
        }

        public override Vector2 BallVelocity
        {
            get => ballVelocity;
            set
            {
                if (ballVelocity != value)
                {
                    ballVelocity = value;
                    OnPropertyChanged();
                }
            }
        }

        public override double BallRadius
        {
            get => ballRadius;
            set
            {
                if (ballRadius != value)
                {
                    ballRadius = value;
                    OnPropertyChanged();
                }
            }
        }

        public override double BallMass
        {
            get => ballMass;
            set
            {
                if (ballMass != value)
                {
                    ballMass = value;
                    OnPropertyChanged();
                }
            }
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
