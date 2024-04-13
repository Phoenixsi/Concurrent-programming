using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Data
{
    public class Ball : AbstractBall
    {
        private readonly int ballID;
        private Vector2 ballPosition;
        private Vector2 ballVelocity;
        private double ballRadius;
        private double ballMass;
        public Brush Color { get; set; }

        public Ball(int ballID, Vector2 ballPosition = new Vector2(), Vector2 ballVelocity = new Vector2(), double ballRadius = 10.0, double ballMass = 10.0)
        {
            this.ballID = ballID;
            this.ballPosition = ballPosition;
            this.ballVelocity = ballVelocity;
            this.ballRadius = ballRadius;
            this.ballMass = ballMass;
        }

        public override int BallID => ballID;

        public override Vector2 BallPosition
        {
            get => ballPosition;
            set
            {
                ballPosition = value;
                OnPropertyChanged("BallPosition");
            }
        }

        public override double BallPositionX
        {
            get => BallPosition.X;
            set
            {
                BallPosition = new Vector2((float)value, BallPosition.Y);
                OnPropertyChanged("BallPositionX");
            }
        }

        public override double BallPositionY
        {
            get => BallPosition.Y;
            set
            {
                BallPosition = new Vector2(BallPosition.X,(float) value);
                OnPropertyChanged("BallPositionY");
            }
        }


        public override Vector2 BallVelocity
        {
            get => ballVelocity;
            set
            {
                ballVelocity = value;
                OnPropertyChanged("BallVelocity");
            }
        }

        public override double BallRadius
        {
            get => ballRadius;
            set => ballRadius = value;
        }

        public override double BallMass
        {
            get => ballMass;
            set => ballMass = value;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
