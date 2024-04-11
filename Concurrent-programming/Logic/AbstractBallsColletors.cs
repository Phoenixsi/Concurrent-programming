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
            

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

       
    }
}
