using System.Data.Entity;
using GeePo;
using GeePoo.Client.EFDbContext;
using GeePoo.Client.ToSupportBlog.AppService;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace GeePoo.Client.ServiceLocator
{
    public static class For
    {
        public static void  MarketPlace(UnityContainer unityContainer)
        {
            unityContainer.RegisterType<IProductService, ProductService>();
            unityContainer.RegisterType<IRepository, GenericRepository>();

            //I have used EntityFramework in my code and following line is quite handy to tell container to use same instance of DbContent for an object graph
            unityContainer.RegisterType<DbContext>(new PerResolveLifetimeManager(), new InjectionFactory(c => new ScalableDbContext("EF.Mapper.dll", "EFTest")));
        }
    }
}