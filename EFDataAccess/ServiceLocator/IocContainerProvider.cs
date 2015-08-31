namespace EFDataAccess.ServiceLocator
{
    //You can change this class to suite your need e.g. return same cached container, return child container or return container based on some argument in ProvideContainer method
    public abstract class IocContainerProvider
    {
        private static IIocContainer _cachedIIocContainer;
        private static readonly object _lockObject = new object();
        
        public IIocContainer ProvideContainer()
        {
            if (_cachedIIocContainer == null)
            {
                lock (_lockObject)
                {
                    if (_cachedIIocContainer == null)
                    {
                        _cachedIIocContainer = ProvideContainer();
                    }
                }
            }

            return _cachedIIocContainer;
            
        }

        protected abstract IIocContainer BuildContainer();
    }
}