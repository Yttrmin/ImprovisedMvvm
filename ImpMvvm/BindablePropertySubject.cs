using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace ImpMvvm
{
    public sealed class BindablePropertySubject<T> : ISubject<T>
    {
        private readonly ReplaySubject<T> Subject = new ReplaySubject<T>(1);

        public BindableProperty<T> BindableProperty => new BindableProperty<T>(this);

        public void OnCompleted() => ((ISubject<T>)Subject).OnCompleted();

        public void OnError(Exception error) => ((ISubject<T>)Subject).OnError(error);

        public void OnNext(T value) => ((ISubject<T>)Subject).OnNext(value);

        public IDisposable Subscribe(IObserver<T> observer) => ((ISubject<T>)Subject).Subscribe(observer);
    }
}
