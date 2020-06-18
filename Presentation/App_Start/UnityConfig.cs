using System.Web.Mvc;
using Application.Interface;
using Application.Services;
using Unity;
using Unity.Mvc5;

namespace Presentation
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			container.RegisterType<IProfileService, ProfileService>();
			container.RegisterType<IProExpService, ProExpService>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}