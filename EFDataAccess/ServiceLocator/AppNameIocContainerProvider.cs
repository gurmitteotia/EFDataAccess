using System.Data.Entity;
using EFDataAccess.Repository;
using EFDataAccess.Repository.EFDbContext;
using EFDataAccess.ToSupportBlog.AppService;
using Microsoft.Practices.Unity;

namespace EFDataAccess.ServiceLocator
{
    public class AppNameIocContainerProvider : IocContainerProvider
    {
        protected override IIocContainer BuildContainer()
        {
            var unityContainer = new UnityContainer();
            
            unityContainer.RegisterType<IProductService, ProductService>();
            unityContainer.RegisterType<IRepository, GenericRepository>();

            //I have used EF in my code and following line is quite handy to tell container that use same instance of DbContent while creating object graph. Very-very useful.
            unityContainer.RegisterType<DbContext>(new PerResolveLifetimeManager(), new InjectionFactory(c => new ScalableDbContext("EF.Mapper.dll", "EFTest")));

            return new UnityIocContainer(unityContainer);
        }
    }
}