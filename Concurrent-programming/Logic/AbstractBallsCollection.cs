using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrent_programming.Logic
{
    public abstract class AbstractBallsColletors : INotifyCollectionChanged, IDisposable
    {
        public abstract int Count { get; }
        public abstract void AddBall();
        public abstract void RemoveBall();
        public abstract void ClearBall();
        public abstract void Dispose();
        public abstract void StartTimer();
        public abstract void StopTimer();
        public abstract void InitBalls(int ballsNumber);
        public abstract void SetNewArea(double width, double height);
        public abstract void SetNewSize(double size);


        public event NotifyCollectionChangedEventHandler? CollectionChanged;

       
    }
}
