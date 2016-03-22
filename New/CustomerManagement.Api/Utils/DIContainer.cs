using CustomerManagement.Logic.Model;
using CustomerManagement.Logic.Utils;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace CustomerManagement.Api.Utils
{
    public static class DIContainer
    {
        private static UnityContainer _container;

        internal static void Init()
        {
            _container = new UnityContainer();

            _container.RegisterType<UnitOfWork, UnitOfWork>(new PerHttpRequestLifetime("UnitOfWork"));
            _container.RegisterType<IEmailGateway, EmailGateway>(new TransientLifetimeManager());
        }

        public static UnityDependencyResolver GetDependencyResolver()
        {
            return new UnityDependencyResolver(_container);
        }

        public static UnitOfWork ResolveUnitOfWork()
        {
            return _container.Resolve<UnitOfWork>();
        }

        public static bool IsUnitOfWorkInstantiated()
        {
            return new PerHttpRequestLifetime("UnitOfWork").GetValue() != null;
        }
    }
}
