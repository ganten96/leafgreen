using System.Collections.Generic;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;
using System.Linq;
using LeafGreen.Infrastructure;

namespace LeafGreen.Autofac
{
    public class RepositoryModule : Module
    {
        private IEnumerable<Assembly> _assemblies;
        public RepositoryModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assemblies.ToArray());
            builder.RegisterType<IRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            //other SqlConnectionBase needs to be here when it is made.
        }

    }
}
