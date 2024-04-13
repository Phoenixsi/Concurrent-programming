using Data;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Collections.Specialized;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Diagnostics;

namespace Logic
{
    public class BallsCollection : AbstractBallsCollection
    {
        public override ObservableCollection<AbstractBall> Balls { get; set; }

        public readonly Timer timer;
        double canvasWidth;
        double canvasHeight;
        double DefRad = 50;

        public BallsCollection(double CanvasWidth = 100.0, double CanvasHeight = 100.0)
        {
            Balls = new ObservableCollection<AbstractBall>();
            this.canvasWidth = CanvasWidth;
            this.canvasHeight = CanvasHeight;

            timer = new Timer(10);
                
            timer.Elapsed += EveryFrame;
            timer.AutoReset = true;
        }

        public override int CountedBalls => Balls.Count;

        private void EveryFrame(object? sender, ElapsedEventArgs e)
        {
            UpdateFrame();
        }

        public void UpdateFrame()
        {
            foreach (Ball ball in Balls)
            {
                double newcordX = ball.BallPosition.X + ball.BallVelocity.X;
                double newcordY = ball.BallPosition.Y + ball.BallVelocity.Y;
                double newvelX = ball.BallVelocity.X;
                double newvelY = ball.BallVelocity.Y;

                if (newcordX > canvasHeight - ball.BallRadius)
                {
                    newvelX = -newvelX;
                    newcordX += newvelX;
                }
                if (newcordX < 0)
                {
                    newvelX = -newvelX;
                    newcordX += newvelX;
                }
                if (newcordY > canvasHeight - ball.BallRadius)
                {
                    newcordY = canvasHeight - ball.BallRadius;
                    newvelY = -newvelY;
                }
                if (newcordY < 0)
                {
                    newcordY = 0;
                    newvelY = -newvelY;
                }

                if (ball.BallPosition.X != newcordX)
                {
                    ball.BallPosition = new Vector2((float)newcordX, ball.BallPosition.Y);
                }

                if (ball.BallPosition.Y != newcordY)
                {
                    ball.BallPosition = new Vector2(ball.BallPosition.X, (float)newcordY);
                }

                if (ball.BallVelocity.X != newvelX)
                {
                    ball.BallVelocity = new Vector2((float)newvelX, ball.BallVelocity.Y);
                }

                if (ball.BallVelocity.Y != newvelY)
                {
                    ball.BallVelocity = new Vector2(ball.BallVelocity.X, (float)newvelY);
                }

                // Log the new position and velocity of the ball
                Debug.WriteLine($"Ball ID: {ball.BallID}, New Position: {ball.BallPosition}, New Velocity: {ball.BallVelocity}");
            }
        }



        public override void AddBall()
        {
            Random random = new Random();
            Vector2 randomPosition;
            bool isOverlapping;

            do
            {
                double randomX = random.NextDouble() * (canvasWidth - 2 * DefRad);
                double randomY = random.NextDouble() * (canvasHeight - 2 * DefRad);
                randomPosition = new Vector2((float)randomX, (float)randomY);

                isOverlapping = Balls.Any(b => Vector2.Distance(b.BallPosition, randomPosition) < 2 * DefRad);
            }
            while (isOverlapping);

            double randomVX = -100 + random.NextDouble() * 100;
            double randomVY = -100 + random.NextDouble() * 100;
            Vector2 randomVelocity = new Vector2((float)randomVX, (float)randomVY);

            AbstractBall ball = new Ball(Balls.Count + 1, randomPosition, randomVelocity, DefRad); // Można dodać masę by niektóre były większe 

            Balls.Add(ball);

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, "AddBall"));
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
            if (index <= Balls.Count)
            {
                Balls.RemoveAt(index);
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, "RemoveBall"));
            }
            else
            {
                throw new ArgumentOutOfRangeException("Usunięcie poza zasięgiem");
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
            if (timer  != null) timer.Start();
        }

        public override void StopTimer()
        {
            timer?.Stop();
        }

        public override void ChangeRadius(double radius)
        {
            DefRad = radius;
        }

        public override void ChangeArea(double x, double y)
        {
            canvasWidth = x;
            canvasHeight = y;
        }

        public override event NotifyCollectionChangedEventHandler? CollectionChanged;

    }
}
