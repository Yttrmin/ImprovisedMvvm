using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace ImpMvvm.CoreSample
{
    public sealed class BasicTextEntriesViewModel : ViewModel<BasicTextEntriesViewModel>
    {
        private readonly BindablePropertySubject<string> EntrySubject = new BindablePropertySubject<string>();

        public BindableProperty<string> EntryProperty => EntrySubject.BindableProperty;
    }
}
