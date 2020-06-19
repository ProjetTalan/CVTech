using System.Web.Mvc;
using Application.Interface;
using Application.Services;
using Infrastructure;
using Unity;
using Unity.Mvc5;

namespace Presentation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IApplicationContext, ApplicationContext>();
            container.RegisterType<IProfileService, ProfileService>();
			container.RegisterType<IProExpService, ProExpService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}