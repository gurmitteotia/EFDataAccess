using System;
using System.Collections.Generic;

namespace EFDataAccess.ServiceLocator
{
    public interface IIocContainer : IDisposable
    {
        T Resolve<T>();

        object Resolve(Type type);

        T Resolve<T>(string name);

        object Resolve(Type type, string name);

        IEnumerable<T> ResolveAll<T>();

        IEnumerable<object> ResolveAll(Type type);

    }
}