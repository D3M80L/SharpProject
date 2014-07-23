using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace MultithreadingExamples.Tests.Infrastructure
{
    /// <summary>
    /// Just a simple idea to test a multithreaded code.
    /// Each code/application is some kind of a state machine.
    /// TBD: explain, check some details etc...
    /// </summary>
    public sealed class StateMachine : IDisposable
    {
        public void SetState(string message)
        {
            ThrowIfDisposed();

            var observers = _observers
                .ToArray();

            foreach (var observer in observers)
            {
                observer.State(state: message);
            }
        }

        private readonly List<IStateObserver> _observers = new List<IStateObserver>();
        public TStateObserver AddObserver<TStateObserver>(TStateObserver stateObserver)
            where TStateObserver : class, IStateObserver
        {
            lock (_observers)
            {
                _observers.Add(stateObserver);
            }
            return stateObserver;
        }

        public void RemoveObserver(IStateObserver stateObserver)
        {
            lock (_observers)
            {
                _observers.Remove(stateObserver);
            }
        }

        [DebuggerHidden]
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new InvalidOperationException("The object was disposed.");
            }
        }

        private bool _disposed = false;
        public void Dispose()
        {
            _disposed = true;
        }
    }
}