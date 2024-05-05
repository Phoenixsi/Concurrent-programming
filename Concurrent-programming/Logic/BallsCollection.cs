// BallsCollection.cs
using Data;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Logic
{
    /// <summary>
    /// Concrete implementation of AbstractBallsCollection.
    /// </summary>
    public class BallsCollection : AbstractBallsCollection
    {
        public override ObservableCollection<AbstractBall> Balls { get; set; }

        private readonly Timer timer;
        private readonly object lockObject = new object();
        private double canvasWidth;
        private double canvasHeight;
        private double defRad = 50.0;

        public BallsCollection(double canvasWidth = 100.0, double canvasHeight = 100.0)
        {
            Balls = new ObservableCollection<AbstractBall>();
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;

            timer = new Timer(20)
            {
                AutoReset = true
            };
            timer.Elapsed += EveryFrame;
        }

        public override int CountedBalls => Balls.Count;

        private void EveryFrame(object? sender, ElapsedEventArgs e)
        {
            UpdateFrame();
        }

        /// <summary>
        /// Updates the positions and velocities of all balls in the collection.
        /// </summary>
        public void UpdateFrame()
        {
            lock (lockObject)
            {
                foreach (Ball ball in Balls)
                {
                    double newBallPositionX = ball.BallPositionX + ball.BallVelocity.X;
                    double newBallPositionY = ball.BallPositionY + ball.BallVelocity.Y;

                    double newBallVelocityX = ball.BallVelocity.X;
                    double newBallVelocityY = ball.BallVelocity.Y;

                    // Collision with the left or right wall
                    if (newBallPositionX < 0)
                    {
                        newBallVelocityX = -newBallVelocityX;
                        newBallPositionX = 0;
                    }
                    else if (newBallPositionX > canvasWidth - ball.BallRadius)
                    {
                        newBallVelocityX = -newBallVelocityX;
                        newBallPositionX = canvasWidth - ball.BallRadius;
                    }

                    // Collision with the top or bottom wall
                    if (newBallPositionY < 0)
                    {
                        newBallVelocityY = -newBallVelocityY;
                        newBallPositionY = 0;
                    }
                    else if (newBallPositionY > canvasHeight - ball.BallRadius)
                    {
                        newBallVelocityY = -newBallVelocityY;
                        newBallPositionY = canvasHeight - ball.BallRadius;
                    }

                    // Update ball position
                    ball.BallPositionX = newBallPositionX;
                    ball.BallPositionY = newBallPositionY;

                    // Update ball velocity
                    ball.BallVelocity = new Vector2((float)newBallVelocityX, (float)newBallVelocityY);

                    // Log the new position and velocity of the ball
                    Debug.WriteLine($"Ball ID: {ball.BallID}, New Position: {ball.BallPosition}, New Velocity: {ball.BallVelocity}");
                }
            }
        }

        public override void AddBall()
        {
            Random random = new Random();
            Vector2 randomPosition;
            bool isOverlapping;

            // Generate a random radius between 10 and 50
            double randomRadius = 10 + random.NextDouble() * 40;
            double randomMass = randomRadius; // Use radius as a proxy for mass

            do
            {
                double randomX = random.NextDouble() * (canvasWidth - 2 * randomRadius);
                double randomY = random.NextDouble() * (canvasHeight - 2 * randomRadius);
                randomPosition = new Vector2((float)randomX, (float)randomY);

                isOverlapping = Balls.Any(b => Vector2.Distance(b.BallPosition, randomPosition) < 2 * randomRadius);
            } while (isOverlapping);

            double randomVX = -10 + random.NextDouble() * 20; // Ensure speed in both directions
            double randomVY = -10 + random.NextDouble() * 20;
            Vector2 randomVelocity = new Vector2((float)randomVX, (float)randomVY);

            AbstractBall ball = new Ball(Balls.Count + 1, randomPosition, randomVelocity, randomRadius, randomMass);

            Balls.Add(ball);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, ball));
        }

        public override void InitBalls(int ballsNumber)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                AddBall();
            }
        }

        public override void RemoveBall(int index)
        {
            if (index < Balls.Count)
            {
                AbstractBall ball = Balls[index];
                Balls.RemoveAt(index);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, ball));
            }
            else
            {
                throw new ArgumentOutOfRangeException("Remove index out of range.");
            }
        }

        public override void Clear()
        {
            if (Balls != null)
            {
                Balls.Clear();
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public override void Dispose()
        {
            Balls.Clear();
            timer?.Stop();
            timer?.Dispose();
        }

        public override void StartTimer()
        {
            timer?.Start();
        }

        public override void StopTimer()
        {
            timer?.Stop();
        }

        public override void ChangeRadius(double radius)
        {
            defRad = radius;
        }

        public override void ChangeArea(double x, double y)
        {
            canvasWidth = x;
            canvasHeight = y;
        }

        public override event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}
