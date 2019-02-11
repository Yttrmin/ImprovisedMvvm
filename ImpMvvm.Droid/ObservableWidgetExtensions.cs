using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace ImpMvvm.Droid
{
    public static class ObservableWidgetExtensions
    {
        public static IObservable<string> TextObservable(this TextView @this) =>
            Observable.Concat(
                    Observable.Return(@this.Text),
                    Observable.FromEventPattern(@this, nameof(TextView.AfterTextChanged)).Select(_ => @this.Text));
    }
}