using Autofac;
using Autofac.Core;
using System;

namespace FinancialTracker.Core.Commons.Utils
{
    public static partial class Asserts
    {
        public static bool IsTypeRegistered(ILifetimeScope lifetimeScope, Type type, out Result result)
        {
            Asserts.ThrowIfNull(type, nameof(type));
            Asserts.ThrowIfNull(lifetimeScope, nameof(lifetimeScope));

            result = Result.Ok();

            if (lifetimeScope.IsRegistered(type))
                return true;

            var exception = new DependencyResolutionException($"{type.FullName} not registered");
            result = Result.Failed(exception);
            return false;
        }
    }
}
