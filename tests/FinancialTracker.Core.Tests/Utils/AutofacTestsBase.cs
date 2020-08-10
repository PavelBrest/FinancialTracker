using Autofac;

namespace FinancialTracker.Core.Tests.Utils
{
    public abstract class AutofacTestsBase
    {
        protected readonly ContainerBuilder _containerBuilder;


        protected AutofacTestsBase()
        {
            _containerBuilder = new ContainerBuilder();
        }
    }
}
