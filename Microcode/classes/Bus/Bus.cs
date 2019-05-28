using System;
using System.Data;
using System.Reactive.Disposables;

namespace Architecture.classes.Bus
{
    public abstract class Bus
    {
        public abstract dynamic Value { get; set; }
    }
}