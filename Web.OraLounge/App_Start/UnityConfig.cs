using AutoMapper;
using Data.OraLounge;
using Domain.OraLounge;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;
using Web.OraLounge.Areas.Admin.Helpers;
using Web.OraLounge.Helpers;
using Web.OraLounge.Services;
using Web.OraLounge.Services.Interfaces;

namespace Web.OraLounge
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IBookingService, BookingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentService, PaymentService>(new HierarchicalLifetimeManager());

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AdminMappingProfile>();
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}