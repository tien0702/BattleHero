using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TT
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, IGameService> _services = new Dictionary<Type, IGameService>();

        private ServiceLocator() { }

        public static ServiceLocator Current { get; private set; }

        public static void Initialize()
        {
            Current = new ServiceLocator();
        }

        public void Register<T>(T service) where T : IGameService
        {
            Type key = typeof(T);

            if (_services.ContainsKey(key))
            {
                Debug.Log($"{key} registered");
            }
            else
            {
                _services.Add(key, service);
            }
        }

        public void Unregister<T>() where T : IGameService
        {
            Type key = typeof(T);

            if (!_services.ContainsKey(key))
            {
                Debug.Log($"{key} is not registered");
            }
            else
            {
                _services.Remove(key);
            }
        }

        public bool IsRegistered<T>()
        {
            return _services.ContainsKey(typeof(T));
        }

        public T Get<T>() where T : IGameService
        {
            Type key = typeof(T);

            if (!_services.ContainsKey(key))
            {
                Debug.Log($"{key} not register");
                return default(T);
            }

            return (T)_services[key];
        }
    }
}
