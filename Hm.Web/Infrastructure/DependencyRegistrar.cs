using Autofac;
using Autofac.Core;
using Hm.Core.Caching;
using Hm.Core.Infrastructure;
using Hm.Core.InfraStructure.DependencyManagement;
using Hm.Web.Controllers;

namespace Hm.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //we cache presentation models between requests
            builder.RegisterType<AccountController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("hm_cache_static"));         
        }

        public int Order
        {
            get { return 2; }
        }
    }
}
