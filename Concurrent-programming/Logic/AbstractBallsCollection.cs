// AbstractBallsCollection.cs
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Abstract base class representing a collection of balls.
    /// </summary>
    public abstract class AbstractBallsCollection : INotifyCollectionChanged, IDisposable
    {
        public abstract ObservableCollection<AbstractBall> Balls { get; set; }
        public abstract int CountedBalls { get; }

        public abstract void AddBall();
        public abstract void RemoveBall(int index);
        public abstract void ChangeRadius(double radius);
        public abstract void ChangeArea(double x, double y);
        public abstract void InitBalls(int ballsNumber);
        public abstract void Dispose();
        public abstract void StartTimer();
        public abstract void StopTimer();
        public abstract void Clear();

        public abstract event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}
