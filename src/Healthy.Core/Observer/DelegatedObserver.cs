using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    internal class DelegatedObserver<T> : IObserver<T>
    {
        private readonly Action<T> _onNext;

        public DelegatedObserver(Action<T> onNext)
        {
            _onNext = onNext;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            _onNext(value);
        }
    }
}
