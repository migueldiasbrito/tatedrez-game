using System;
using System.Collections.Generic;

namespace Mdb.Tatedrez.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        public void Publish<T>(T notification) where T : INotification
        {
            Type type = typeof(T);
            if (!_subscribers.TryGetValue(type, out List<Delegate> subscribers)) return;

            foreach (Delegate callback in subscribers)
            {
                ((Action<T>) callback)(notification);
            }
        }

        public void Subscribe<T>(Action<T> callback) where T : INotification
        {
            Type type = typeof(T);
            if (!_subscribers.TryGetValue(type, out List<Delegate> subscribers))
            {
                subscribers = new List<Delegate>();
                _subscribers.Add(type, subscribers);
            }

            subscribers.Add(callback);
        }

        public void Unsubscribe<T>(Action<T> callback) where T : INotification
        {
            Type type = typeof(T);
            if (!_subscribers.TryGetValue(type, out List<Delegate> subscribers)) return;

            subscribers.Remove(callback);

            if (subscribers.Count == 0) _subscribers.Remove(type);
        }
    }
}
