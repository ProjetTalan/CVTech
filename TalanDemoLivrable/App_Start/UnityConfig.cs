using System.Web.Mvc;
using DataService.Interface;
using DataService.Services;
using Unity;
using Unity.Mvc5;

namespace TalanDemoLivrable
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

			container.RegisterType<ICivilStateService, CivilStateService>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}