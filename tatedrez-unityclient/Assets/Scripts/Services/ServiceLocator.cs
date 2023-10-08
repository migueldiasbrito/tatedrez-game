using System;
using System.Collections.Generic;

namespace Mdb.Tatedrez.Services
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> _services = new();

        public static T Get<T>() where T : IService
        {
            return (T) _services[typeof(T)];
        }

        public static void Bind<T>(IService service) where T : IService
        {
            _services.Add(typeof(T), service);
        }
    }
}
