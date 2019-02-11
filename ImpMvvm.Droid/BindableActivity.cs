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

namespace ImpMvvm.Droid
{
    public abstract class BindableActivity<TViewModel> : Activity where TViewModel : IViewModel
    {
        private TViewModel ViewModel;
        private readonly List<Binding> Bindings = new List<Binding>();

        public void BindOneWayToViewModel<T>(Func<TViewModel, BindableProperty<T>> propertySelector, IObservable<T> viewObservable)
        {
            // TODO - Check if property is indeed a member of ViewModel.
            // TODO - Check if binding is currently allowed
            var bindableProperty = propertySelector(ViewModel);
            var binding = OneWayToViewModelBinding<T>.FromBindableProperty(bindableProperty, viewObservable, ViewModel);
            Bindings.Add(binding);
        }
    }
}