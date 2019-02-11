using System;
using System.Collections.Generic;
using System.Text;

namespace ImpMvvm
{
    public struct BindableProperty<T>
    {
        internal IObservable<T> Observable { get; }

        internal BindableProperty(IObservable<T> observable)
        {
            Observable = observable;
        }
    }
}
