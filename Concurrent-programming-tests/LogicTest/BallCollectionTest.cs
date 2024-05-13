using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest
{
    [TestClass]
    public class BallCollectionTest
    {
        [TestMethod]
        public void InitBallsTest()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            int initialBalls = 20;
            ballsCollection.InitBalls(initialBalls);
            Assert.AreEqual(initialBalls, ballsCollection.CountedBalls);
        }

        [TestMethod]
        public void AddBallsTest()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            ballsCollection.AddBall();
            Assert.AreEqual(1, ballsCollection.CountedBalls);
            ballsCollection.AddBall();
            Assert.AreEqual(2, ballsCollection.CountedBalls);
        }

        [TestMethod]
        public void RemoveBallsTest()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            ballsCollection.AddBall();
            ballsCollection.AddBall();
            ballsCollection.RemoveBall(1);
            Assert.AreEqual(1, ballsCollection.CountedBalls);
            ballsCollection.RemoveBall(0);
            Assert.AreEqual(0, ballsCollection.CountedBalls);
        }

        [TestMethod]
        public void ClearBallsTest()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            ballsCollection.AddBall();
            ballsCollection.AddBall();
            ballsCollection.Dispose();
            Assert.AreEqual(0, ballsCollection.CountedBalls);
        }

        [TestMethod]
        public async Task PositionAfterFrameUpdateTest()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            ballsCollection.AddBall();
            var ballBefore = ballsCollection.Balls[0];
            ballsCollection.StartTimer();
            await Task.Delay(40);  // Wait two frame intervals to ensure the timer has elapsed at least once
            ballsCollection.StopTimer();
            Assert.AreNotEqual(ballBefore.BallPosition, ballsCollection.Balls[0].BallPosition);
            Assert.AreNotEqual(ballBefore.BallVelocity, ballsCollection.Balls[0].BallVelocity);
        }

        [TestMethod]
        public void CollectionChangeEventTest()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            List<string> eventNames = new List<string>();
            ballsCollection.CollectionChanged += (sender, e) => eventNames.Add(e.Action.ToString());
            ballsCollection.AddBall();
            ballsCollection.AddBall();
            Assert.AreEqual(2, eventNames.Count);  // Check if two Add actions were recorded
            ballsCollection.RemoveBall(0);
            Assert.AreEqual(3, eventNames.Count);  // Check if a Remove action was recorded
            ballsCollection.Clear();
            Assert.AreEqual(4, eventNames.Count);  // Check if a Reset action was recorded
        }
    }
}
