using System.Collections.Generic;

namespace YTools
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ??= new();

        private readonly Dictionary<string, IService> _services = new();

        public void Clear() => _services.Clear();

        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;

            if (!_services.ContainsKey(key))
            {
                UnityEngine.Debug.LogError($"{key.Color(ColorType.Cyan)} not services");
                return default;
            }

            return (T)_services[key];
        }

        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;

            if (_services.ContainsKey(key))
            {
                UnityEngine.Debug.LogError($"Attemp to register service <color=green>{key.Color(ColorType.Cyan)}</color>");
                return;
            }

            _services.Add(key, service);
        }

        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;

            if (!_services.ContainsKey(key))
            {
                UnityEngine.Debug.LogError($"Attemp to unregister service <color=green>{key.Color(ColorType.Cyan)}</color>");
                return;
            }

            _services.Remove(key);
        }
    }
}