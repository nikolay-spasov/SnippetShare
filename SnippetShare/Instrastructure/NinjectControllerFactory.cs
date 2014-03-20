namespace SnippetShare.Instrastructure
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Ninject;
    using SnippetShare.Domain.Repositories.Abstract;
    using SnippetShare.Domain.Repositories.Concrete;
    using SnippetShare.Instrastructure.WebSecurity;
    using SnippetShare.Instrastructure.WebSecurity.Concrete;

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel kernel;

        public NinjectControllerFactory()
        {
            this.kernel = new StandardKernel();
            this.AddBindings();
        }

        private void AddBindings()
        {
            this.kernel.Bind<ISnippetRepository>().To<SnippetRepository>();
            this.kernel.Bind<IWebSecurity>().To<WebSecurityWrapper>();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return kernel.Get(controllerType) as IController;
        }
    }
}