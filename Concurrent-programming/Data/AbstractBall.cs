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
        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract int BallID { get; }
        public abstract Vector<double> BallPosition { get; set; }
        public abstract Vector<double> BallVelocity { get; set; }
        public abstract double BallSize { get; set; }
        public abstract double BallMass { get; set; }

    }
}
