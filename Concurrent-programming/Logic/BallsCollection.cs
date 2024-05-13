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

            timer = new Timer(20);

            timer.Elapsed += EveryFrame;
            timer.AutoReset = true;
        }

        public override int CountedBalls => Balls.Count;

        private async void EveryFrame(object? sender, ElapsedEventArgs e)
        {
            await UpdateFrame();
        }

        public Task UpdateFrame()
        {
            // Update positions based on velocities
            foreach (Ball ball in Balls)
            {
                ball.BallPositionX += ball.BallVelocity.X;
                ball.BallPositionY += ball.BallVelocity.Y;
            }

            // Handle ball-wall collisions and correct positions
            foreach (Ball ball in Balls)
            {
                if (ball.BallPositionX <= 0)
                {
                    Vector2 newVelocity = ball.BallVelocity;
                    newVelocity.X = -ball.BallVelocity.X;
                    ball.BallVelocity = newVelocity;
                    ball.BallPositionX = 0; // Correct position
                }
                if (ball.BallPositionX >= canvasWidth - ball.BallRadius)
                {
                    Vector2 newVelocity = ball.BallVelocity;
                    newVelocity.X = -ball.BallVelocity.X;
                    ball.BallVelocity = newVelocity;
                    ball.BallPositionX = canvasWidth - ball.BallRadius; // Correct position
                }
                if (ball.BallPositionY <= 0)
                {
                    Vector2 newVelocity = ball.BallVelocity;
                    newVelocity.Y = -ball.BallVelocity.Y;
                    ball.BallVelocity = newVelocity;
                    ball.BallPositionY = 0; // Correct position
                }
                if (ball.BallPositionY >= canvasHeight - ball.BallRadius)
                {
                    Vector2 newVelocity = ball.BallVelocity;
                    newVelocity.Y = -ball.BallVelocity.Y;
                    ball.BallVelocity = newVelocity;
                    ball.BallPositionY = canvasHeight - ball.BallRadius; // Correct position
                }
            }

            //Handle ball-ball collisions
            for (int i = 0; i < Balls.Count; i++)
            {
                for (int j = i + 1; j < Balls.Count; j++)
                {
                    Ball ball1 = (Ball)Balls[i];
                    Ball ball2 = (Ball)Balls[j];

                    var ball1Pos = ball1.BallPosition + new Vector2((float)ball1.BallRadius / 2);
                    var ball2Pos = ball2.BallPosition + new Vector2((float)ball2.BallRadius / 2);

                    Vector2 distanceVector = ball1Pos - ball2Pos;
                    float distance = distanceVector.Length();
                    float sumRadius = (float)(ball1.BallRadius/2 + ball2.BallRadius/2);

                    if (distance < sumRadius)
                    {
                        Vector2 normal = Vector2.Normalize(distanceVector);
                        Vector2 relativeVelocity = ball1.BallVelocity - ball2.BallVelocity;
                        float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

                        if (velocityAlongNormal > 0) continue;

                        float restitution = 1.0f; // Perfectly elastic collision
                        float impulseMagnitude = (float)(-(1 + restitution) * velocityAlongNormal / (1 / ball1.BallMass + 1 / ball2.BallMass));

                        Vector2 impulse = impulseMagnitude * normal;
                        Vector2 newVelocity1 = ball1.BallVelocity + impulse / (float)ball1.BallMass;
                        Vector2 newVelocity2 = ball2.BallVelocity - impulse / (float)ball2.BallMass;

                        ball1.BallVelocity = newVelocity1;
                        ball2.BallVelocity = newVelocity2;

                        // Prevent overlap jitter
                        float overlap = sumRadius - distance;
                        Vector2 correction = normal * overlap / 2.0f;
                        ball1.BallPosition += correction;
                        ball2.BallPosition -= correction;
                    }
                }
            }

            return Task.CompletedTask;
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

            double randomVX = -10 + random.NextDouble() * 10;
            double randomVY = -10 + random.NextDouble() * 10;
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
            if (timer != null) timer.Start();
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
