using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace ImpMvvm
{
    public sealed class OneWayToViewModelProperty<T>
    {
        private readonly ISubject<T> ViewModelSubject;

        internal OneWayToViewModelProperty(ISubject<T> viewModelSubject)
        {
            ViewModelSubject = viewModelSubject;
        }

        public IDisposable Bind(IObservable<T> viewPropertyObservable)
        {
            throw new NotImplementedException();
        }

        public OneWayToViewModelProperty<T> WithDebugName(string debugName)
        {
            return this;
        }
    }
}