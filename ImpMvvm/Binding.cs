using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Text;

namespace ImpMvvm
{
    public abstract class Binding
    {

    }

    public abstract class Binding<TViewModel, TView, T> : Binding
        where TViewModel : IObservable<T>
        where TView : IObservable<T>
    {
        protected readonly TViewModel ViewModelObservable;
        protected readonly TView ViewObservable;

        protected Binding(TViewModel viewModelObservable, TView viewObservable)
        {
            ViewModelObservable = viewModelObservable;
            ViewObservable = viewObservable;
        }
    }

    internal sealed class OneWayToViewModelBinding<T> : Binding<ISubject<T>, IObservable<T>, T>
    {
        private OneWayToViewModelBinding(ISubject<T> viewModelObservable, IObservable<T> viewObservable, IViewModel viewModel)
            : base(viewModelObservable, viewObservable)
        {
            // TODO - ObserveOn background thread to free up UI thread?
            viewObservable.Subscribe(OnNext, viewModel.CancellationToken);

            void OnNext(T value)
            {
                Debug.WriteLine($"Emitting value '{value}' from view to viewmodel.");
                viewModelObservable.OnNext(value);
            }
        }

        public static OneWayToViewModelBinding<T> FromBindableProperty(BindableProperty<T> viewModelProperty, IObservable<T> viewObservable, IViewModel viewModel)
        {
            if (!(viewModelProperty.Observable is ISubject<T> viewModelSubject))
            {
                throw new ArgumentException($"{nameof(BindableProperty<T>)} must be backed by an {nameof(ISubject<T>)} (preferably a {nameof(BindablePropertySubject<T>)}) in order to be bound OneWayToViewModel.");
            }

            return new OneWayToViewModelBinding<T>(viewModelSubject, viewObservable, viewModel);
        }
    }
}
