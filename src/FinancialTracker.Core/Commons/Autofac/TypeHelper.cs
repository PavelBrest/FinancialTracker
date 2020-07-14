using System;
using System.Reflection;

namespace FinancialTracker.Core.Commons.Autofac
{
    public static class TypeHelper
    {
        public static bool IsAssignableTo(this Type @this, Type type)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.GetTypeInfo().IsAssignableFrom(@this.GetTypeInfo());
        }
    }
}
