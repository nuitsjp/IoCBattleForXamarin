using DryIoc;
using IoCBattleForXamarin.Models;

namespace IoCBattleForXamarin.Containers
{
    public class DryIocContainer : IContainer
    {
        private Container _container;
        public string Name { get; } = "DryIoc.dll";

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public void SetupForTransientTest()
        {
            Setup(Reuse.Transient);
        }

        public void SetupForSingletonTest()
        {
            Setup(Reuse.Singleton);
        }

        private void Setup(IReuse reuse)
        {
            _container = new Container();
            _container.Register<IRepository, Repository>(reuse);
            _container.Register<IAuthenticationService, AuthenticationService>(reuse);
            _container.Register<UserController, UserController>(reuse);

            _container.Register<IWebService, WebService>(reuse);
            _container.Register<IAuthenticator, Authenticator>(reuse);
            _container.Register<IStockQuote, StockQuote>(reuse);
            _container.Register<IDatabase, Database>(reuse);
            _container.Register<IErrorHandler, ErrorHandler>(reuse);

            _container.Register<IService1, Service1>(reuse);
            _container.Register<IService2, Service2>(reuse);
            _container.Register<IService3, Service3>(reuse);
            _container.Register<IService4, Service4>(reuse);

            _container.Register<ILogger, Logger>(reuse);
        }
    }
}
