using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class AbstractBall : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;

        public abstract int BallID { get; }
        public abstract Vector2 BallPosition { get; set; }
        public abstract Vector2 BallVelocity { get; set; }
        public abstract double BallRadius { get; set; }
        public abstract double BallMass { get; set; }

        public abstract float BallPositionX { get; set; }
        public abstract float BallPositionY { get; set; }
    }
}