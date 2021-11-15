using API.OraLounge.Helpers;
using AutoMapper;
using Data.OraLounge;
using Domain.OraLounge;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace API.OraLounge
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}