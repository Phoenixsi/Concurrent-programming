using Data;
using Logic;
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
            List<string> eventName = new List<string>();

            int initialBalls = 20;

            ballsCollection.InitBalls(initialBalls);

            Assert.IsTrue(ballsCollection.CountedBalls.Equals(initialBalls));
        }


        [TestMethod]
        public void AddBallsTest()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);

            ballsCollection.AddBall();
            Assert.IsTrue(ballsCollection.CountedBalls.Equals(1));

            ballsCollection.AddBall();
            Assert.IsTrue(ballsCollection.CountedBalls.Equals(2));

        }

        [TestMethod]
        public void RemoveBallsTest() 
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);

            ballsCollection.AddBall();
            ballsCollection.AddBall();

            ballsCollection.RemoveBall(1);
            Assert.IsTrue(ballsCollection.CountedBalls.Equals(1));
            ballsCollection.RemoveBall(0);
            Assert.IsTrue(ballsCollection.CountedBalls.Equals(0));
        }

        [TestMethod]
        public void ClearBallsTest() 
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);

            ballsCollection.AddBall();
            ballsCollection.AddBall();

            ballsCollection.Dispose();

            Assert.IsTrue(ballsCollection.CountedBalls.Equals(0));
        }

/*        [TestMethod]
        public async Task PostionAfterFrameUpdate()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            ballsCollection.AddBall();

            AbstractBall ballBefor = ballsCollection.Balls[0];

            ballsCollection.StartTimer();
            await Task.Delay(20);
            ballsCollection.StopTimer();

            Assert.AreNotEqual(ballBefor.BallPosition, ballsCollection.Balls[0].BallPosition);
            Assert.AreNotEqual(ballBefor.BallVelocity, ballsCollection.Balls[0].BallVelocity);
        }
*/

        [TestMethod]
        public void CollectionChange()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);

            List<string> eventName = new List<string>();

            ballsCollection.CollectionChanged += (sender, change) => {
                eventName.Add(change.ToString());
            };

            ballsCollection.AddBall();
            ballsCollection.AddBall();
            Assert.IsTrue(eventName.Count == 2);

            ballsCollection.RemoveBall(0);
            Assert.IsTrue(eventName.Count == 3);

            
            ballsCollection.Clear();
            Assert.IsTrue(eventName.Count == 4);

/*
            ballsCollection.StartTimer();
            await Task.Delay(10);
            ballsCollection.StopTimer();
*/



        }

    }
}
