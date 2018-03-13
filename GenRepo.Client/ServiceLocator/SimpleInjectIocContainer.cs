using System;
using System.Collections.Generic;
using SimpleInjector;

namespace GenRepo.Client.ServiceLocator
{
    internal class SimpleInjectIocContainer : IocContainer
    {
        private readonly Container _container;
        public SimpleInjectIocContainer(Container container)
        {
            _container = container;
        }

        public override T Resolve<T>()
        {
            return _container.GetInstance<T>();
        }
        public override object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }
        public override IEnumerable<T> ResolveAll<T>()
        {
            return _container.GetAllInstances<T>();
        }

        public override IEnumerable<object> ResolveAll(Type type)
        {
            return _container.GetAllInstances(type);
        }

        public override void Dispose()
        {
            _container.Dispose();
        }
    }
}