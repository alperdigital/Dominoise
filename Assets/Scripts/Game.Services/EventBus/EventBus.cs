using System;
using System.Collections.Generic;

// Namespace'i Game.Core'dan Game.Services'e güncellendi
namespace Game.Services 
{
    // Marker interface for events
    public interface IEvent
    {
    }

    public interface IEventBus
    {
        void Subscribe<T>(Action<T> handler);
        void Unsubscribe<T>(Action<T> handler);
        void Publish<T>(T evt);
    }

    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> _delegates = new Dictionary<Type, Delegate>();

        public void Subscribe<T>(Action<T> handler)
        {
            var key = typeof(T);
            if (_delegates.ContainsKey(key))
            {
                _delegates[key] = Delegate.Combine(_delegates[key], handler);
            }
            else
            {
                _delegates[key] = handler;
            }
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var key = typeof(T);
            if (_delegates.ContainsKey(key))
            {
                var result = Delegate.Remove(_delegates[key], handler);
                if (result == null)
                {
                    _delegates.Remove(key);
                }
                else
                {
                    _delegates[key] = result;
                }
            }
        }

        public void Publish<T>(T evt)
        {
            if (evt == null) return;
            
            var key = typeof(T);
            if (_delegates.TryGetValue(key, out var handler))
            {
                (handler as Action<T>)?.Invoke(evt);
            }
        }
    }
}