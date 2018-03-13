using System;
using System.Collections.Generic;
using Unity;

namespace GenRepo.Client.ServiceLocator
{
    internal class UnityIocContainer : IocContainer
    {
        private readonly UnityContainer _unityContainer;

        public UnityIocContainer(UnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }
        public override T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public override object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }
        public override IEnumerable<T> ResolveAll<T>()
        {
            return _unityContainer.ResolveAll<T>();
        }

        public override IEnumerable<object> ResolveAll(Type type)
        {
            return _unityContainer.ResolveAll(type);
        }
        public override void Dispose()
        {
            _unityContainer.Dispose();
        }
    }
}