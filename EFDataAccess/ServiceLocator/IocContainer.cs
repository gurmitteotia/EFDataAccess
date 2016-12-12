using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace EFDataAccess.ServiceLocator
{
    public abstract class IocContainer : IDisposable
    {
        private static IocContainer _unityContainer;

        public abstract T Resolve<T>() where T : class;
        public abstract object Resolve(Type type);
        public abstract IEnumerable<T> ResolveAll<T>() where T : class;
        public abstract IEnumerable<object> ResolveAll(Type type);
        public abstract void Dispose();

        /*
         * Sytax for creating container= IocContainer.Unity(For.MarketPlace);
         * 
         */

        public static IocContainer Unity(Action<UnityContainer> configure)
        {
            //not multithread safe
            if (_unityContainer != null)
                return _unityContainer;
            var container = new UnityContainer();
            configure(container);
            return _unityContainer = new UnityIocContainer(container);
        }
    }
}