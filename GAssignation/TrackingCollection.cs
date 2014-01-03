// -----------------------------------------------------------------------
// <copyright file="TrackingCollection.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace GAssignation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.Collections;

    public class TrackingCollection<T> : IEnumerable<T>
    {
        readonly List<T> innerCollection = new List<T>();
        public event Action<T> ItemAdded;

        public void Add(T item)
        {
            innerCollection.Add(item);

            if (ItemAdded != null)
            {
                ItemAdded(item);
            }
        }

        public IDisposable OnItemAdded(Action<T> onItemAdded)
        {
            ItemAdded += onItemAdded;
            return new EventDisposable(this, onItemAdded);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class EventDisposable : IDisposable
        {
            readonly TrackingCollection<T> parent;
            readonly Action<T> handler;

            public EventDisposable(TrackingCollection<T> parent, Action<T> handler)
            {
                this.parent = parent;
                this.handler = handler;
            }

            public void Dispose()
            {
                parent.ItemAdded -= handler;
            }
        }
    }
}
