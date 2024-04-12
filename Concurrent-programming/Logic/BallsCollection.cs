using Data;
using System.Collections.ObjectModel;
using System.Timers;
using System.Numerics;
using System;
using System.Collections.Specialized;

namespace Logic
{
    public class BallsCollection : AbstractBallsCollection
    {
        public override ObservableCollection<AbstractBall> Balls { get; set; }

        readonly System.Timers.Timer? timer;
        double CanvaWidth = 0;
        double CanvaHeight = 0;
        double DefRad = 50;

        public BallsCollection(double CanvaWidth = 0.0, double CanvaHeight = 0.0)
        {
            this.CanvaWidth = CanvaWidth;
            this.CanvaHeight = CanvaHeight;

            Balls = new ObservableCollection<AbstractBall>();
            timer = new System.Timers.Timer(6.94);
                
            timer.Elapsed += EveryFrame;
        }

        public override int CountedBalls => Balls.Count;

        public void EveryFrame(object? sender, ElapsedEventArgs e)
        {
            UpdateFrame();
        }

        public void UpdateFrame()
        {
            foreach (Ball ball in Balls)
            {
                ball.BallPosition += ball.BallVelocity;
            }
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, "UpdateFrame"));
        }

        public override void AddBall()
        {
            double randomX = DefRad + new Random().NextDouble() * (CanvaWidth - 2 * DefRad);
            double randomY = DefRad + new Random().NextDouble() * (CanvaHeight - 2 * DefRad);
            Vector2 randomPosition = new Vector2((float)randomX, (float)randomY);

            double randomVX = -5 + new Random().NextDouble() * 10;
            double randomVY = -5 + new Random().NextDouble() * 10;
            Vector2 randomVelocity = new Vector2((float)randomVX, (float)randomVY);

            AbstractBall ball = new Ball(Balls.Count + 1, randomPosition, randomVelocity, DefRad); // Można dodać masę by niektóre były większe 

            Balls.Add(ball);

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, "AddBall"));
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

        public override void InitBalls(int ballsNumber)
        {
            for (int i = 0; i < ballsNumber; i++)
            {
                AddBall();
            }
        }

        public override void Dispose()
        {
            Balls.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, "Dispose"));

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

        public override event NotifyCollectionChangedEventHandler? CollectionChanged;

    }
}
