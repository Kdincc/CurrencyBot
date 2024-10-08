﻿namespace CurrencyBot.BL
{
    public class Unsubscriber<T>(List<IObserver<T>> observers, IObserver<T> observer) : IDisposable
    {
        private readonly List<IObserver<T>> _observers = observers;
        private readonly IObserver<T> _observer = observer;

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }

}
