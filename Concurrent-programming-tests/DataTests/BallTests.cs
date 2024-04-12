using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;

namespace BallTests
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void getTests()
        {
            int ballID = 0;

            Vector2 ballPosition = new Vector2(10, 5);
            Vector2 ballVelocity = new Vector2(1, 2);
            double ballRadius = 10.0;
            double ballMass = 5.0;

            AbstractBall Ball = new Ball(ballID, ballPosition, ballVelocity, ballRadius, ballMass);

            Assert.IsNotNull(Ball);
            Assert.AreEqual(Ball.BallID, ballID);
            Assert.AreEqual(Ball.BallPosition, ballPosition);
            Assert.AreEqual(Ball.BallVelocity, ballVelocity);
            Assert.AreEqual(Ball.BallRadius, ballRadius);
            Assert.AreEqual(Ball.BallMass, ballMass);

        }

        [TestMethod]
        public void setTests()
        {
            int ballID = 0;
            Vector2 ballPosition = new Vector2(10, 5);
            Vector2 ballVelocity = new Vector2(1, 2);
            double ballRadius = 10.0;
            double ballMass = 5.0;

            Vector2 ballPositionChanged = new Vector2(23, 1);
            Vector2 ballVelocityChanged = new Vector2(5252, 51);
            double ballRadiusChanged = 50.0;
            double ballMassChanged = 4.0;

            AbstractBall Ball = new Ball(ballID, ballPosition, ballVelocity, ballRadius, ballMass);

            Ball.BallPosition = ballPositionChanged;
            Ball.BallVelocity = ballVelocityChanged;
            Ball.BallRadius = ballRadiusChanged;
            Ball.BallMass = ballMassChanged;

            Assert.IsNotNull(Ball);
            Assert.AreEqual(Ball.BallPosition, ballPositionChanged);
            Assert.AreEqual(Ball.BallVelocity, ballVelocityChanged);
            Assert.AreEqual(Ball.BallRadius, ballRadiusChanged);
            Assert.AreEqual(Ball.BallMass, ballMassChanged);

        }

        [TestMethod]
        public void propertyChangeTests()
        {
            List<string> eventNames = new List<string>();
            AbstractBall Ball = new Ball(ballID: 1);


            Ball.PropertyChanged += (sender, change) => { 
                if(change.PropertyName != null) 
                    eventNames.Add(change.PropertyName);
            };

            Vector2 newPosition = new Vector2(10, 5);
            Vector2 newVelocity = new Vector2(50, 12);
            
            Assert.IsNotNull(Ball);
            Ball.BallPosition = newPosition;
            Assert.AreEqual(Ball.BallPosition, newPosition);
            Assert.IsTrue(eventNames.Count == 1);
            Assert.IsTrue(eventNames[0] == "BallPosition");
            Ball.BallVelocity = newVelocity;
            Assert.AreEqual(Ball.BallVelocity, newVelocity);
            Assert.IsTrue(eventNames.Count == 2);
            Assert.IsTrue(eventNames[1] == "BallVelocity");



        }
    }
}
