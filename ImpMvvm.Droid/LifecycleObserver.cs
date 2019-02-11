using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Arch.Lifecycle;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;

namespace ImpMvvm.Droid
{
    internal sealed class LifecycleObserver : Java.Lang.Object, ILifecycleObserver
    {
        private readonly Action<Lifecycle.Event> OnEvent;

        public LifecycleObserver(Action<Lifecycle.Event> onEvent)
        {
            OnEvent = onEvent;
        }

        [Lifecycle.Event.OnAny]
        [Export]
        void OnAnyEvent(ILifecycleOwner owner, Lifecycle.Event @event)
        {
            OnEvent(@event);
        }
    }
}