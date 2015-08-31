using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace EFDataAccess.ServiceLocator
{
    public class UnityIocContainer : IIocContainer
    {
        private readonly UnityContainer _unityContainer;

        public UnityIocContainer(UnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public T Resolve<T>(string name)
        {
            return _unityContainer.Resolve<T>(name);
        }

        public object Resolve(Type type, string name)
        {
            return _unityContainer.Resolve(type, name);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _unityContainer.ResolveAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _unityContainer.ResolveAll(type);
        }

        public void Dispose()
        {
            _unityContainer.Dispose();
        }
    }
}