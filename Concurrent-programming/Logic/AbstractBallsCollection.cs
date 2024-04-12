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
    public abstract class AbstractBallsCollection : INotifyCollectionChanged, IDisposable
    {
        public abstract ObservableCollection<AbstractBall> Balls { get; set; }
        public abstract int CountedBalls { get; }
        public abstract void AddBall();
        public abstract void RemoveBall(int index);
        public abstract void InitBalls(int ballsNumber);
        public abstract void Dispose();
        public abstract void StartTimer();
        public abstract void StopTimer();


        public abstract event NotifyCollectionChangedEventHandler? CollectionChanged;

       
    }
}
