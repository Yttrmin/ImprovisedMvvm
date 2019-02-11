using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Arch.Lifecycle;
using System.Reactive.Linq;

namespace ImpMvvm.Droid
{
    // TODO - Make functionality composable?
    // TODO - Do not require a default constructor for viewmodels.
    public abstract class BindableActivity<TViewModel> : AppCompatActivity where TViewModel : IViewModel, new()
    {
        private bool CanPerformBinding;
        private TViewModel ViewModel;
        private readonly List<Binding> Bindings = new List<Binding>();
        private readonly ReplaySubject<Lifecycle.Event> LifecycleSubject = new ReplaySubject<Lifecycle.Event>(1);

        public IObservable<Lifecycle.Event> LifecycleObservable => LifecycleSubject.AsObservable();

        public BindableActivity()
        {
            ViewModel = new TViewModel();
            Lifecycle.AddObserver(new LifecycleObserver(LifecycleSubject.OnNext));
            LifecycleObservable.Subscribe(e => System.Diagnostics.Debug.WriteLine($"New event: {e}"));
            LifecycleObservable.Where(e => e == Lifecycle.Event.OnResume).Subscribe(PerformBinding);

            void PerformBinding(Lifecycle.Event _)
            {
                CanPerformBinding = true;
                try
                {
                    Bindings.AddRange(BindToViewModel());
                }
                finally
                {
                    CanPerformBinding = false;
                }
            }
        }

        public void BindOneWayToViewModel<T>(Func<TViewModel, BindableProperty<T>> propertySelector, IObservable<T> viewObservable)
        {
            // TODO - Check if property is indeed a member of ViewModel.
            if (!CanPerformBinding)
            {
                throw new InvalidOperationException($"Binding methods (including {nameof(BindOneWayToViewModel)} can only be called from an implementation of {nameof(BindToViewModel)}");
            }

            var bindableProperty = propertySelector(ViewModel);
            var binding = OneWayToViewModelBinding<T>.FromBindableProperty(bindableProperty, viewObservable, ViewModel);
            Bindings.Add(binding);
        }

        protected abstract IEnumerable<Binding> BindToViewModel();
    }
}