using Data;
using Logic;
using System;
using System.Collections.Generic;
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

        [TestMethod]
        public async void PostionAfterFrameUpdate()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);
            ballsCollection.AddBall();

            AbstractBall ballBefor = ballsCollection.Balls[0];

            ballsCollection.StartTimer();
            await Task.Delay(10);
            ballsCollection.StopTimer();

            Assert.AreNotEqual(ballBefor.BallPosition, ballsCollection.Balls[0].BallPosition);
            Assert.AreNotEqual(ballBefor.BallVelocity, ballsCollection.Balls[0].BallVelocity);
            Assert.AreEqual(ballBefor.BallPosition + ballBefor.BallVelocity, ballsCollection.Balls[0].BallPosition);
        }

        [TestMethod]
        public async void CollectionChange()
        {
            AbstractBallsCollection ballsCollection = new BallsCollection(500, 800);

            List<string> eventName = new List<string>();

            ballsCollection.CollectionChanged += (sender, change) => {
                if (change.NewItems != null)
                {
                    foreach (var item in change.NewItems)
                    {
                        eventName.Add(item.ToString());
                    }
                }
            };

            ballsCollection.AddBall();
            ballsCollection.AddBall();
            Assert.IsTrue(eventName.Count == 2);
            Assert.IsTrue(eventName[0] == "AddBall");
            Assert.IsTrue(eventName[1] == "AddBall");

            ballsCollection.RemoveBall(0);
            Assert.IsTrue(eventName.Count == 1);
            Assert.IsTrue(eventName[2] == "RemoveBall");
            
            ballsCollection.Dispose();
            Assert.IsTrue(eventName.Count == 0);
            Assert.IsTrue(eventName[3] == "Dispose");

            ballsCollection.StartTimer();
            await Task.Delay(10);
            ballsCollection.StopTimer();


            Assert.IsTrue(eventName.Count() == 1);
            Assert.IsTrue(eventName[4] == "UpdateFrame");


        }

    }
}
