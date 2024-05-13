using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BallUpdate
    {
        public int BallIndex { get; private set; }
        public double NewPositionX { get; private set; }
        public double NewPositionY { get; private set; }
        public double NewVelocityX { get; private set; }
        public double NewVelocityY { get; private set; }

        public BallUpdate(int ballIndex, double newPositionX, double newPositionY, double newVelocityX, double newVelocityY)
        {
            BallIndex = ballIndex;
            NewPositionX = newPositionX;
            NewPositionY = newPositionY;
            NewVelocityX = newVelocityX;
            NewVelocityY = newVelocityY;
        }
    }
}
