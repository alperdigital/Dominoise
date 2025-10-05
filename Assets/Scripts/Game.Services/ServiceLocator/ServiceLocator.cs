using System;
using System.Collections.Generic;

namespace Game.Services
{
    public static class Service
    {
        private static readonly Dictionary<Type, object> _map = new Dictionary<Type, object>();

        public static void Register<T>(T instance)
        {
            _map[typeof(T)] = instance;
        }

        public static T Get<T>()
        {
            var key = typeof(T);
            if (_map.TryGetValue(key, out var instance))
            {
                return (T)instance;
            }
            // Servis bulunamazsa null döndürmek daha güvenlidir.
            return default(T); 
        }
        
        // İsteğe bağlı: Bir servisin kaydını silmek için
        public static void Unregister<T>()
        {
            _map.Remove(typeof(T));
        }
    }
}