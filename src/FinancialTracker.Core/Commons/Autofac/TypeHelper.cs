using FinancialTracker.Core.Commons.Utils;
using System;
using System.Linq;

namespace FinancialTracker.Core.Commons.Autofac
{
    public static class TypeHelper
    {
        public static bool IsAssignableTo(this Type @this, Type type)
        {
            Asserts.ThrowIfNull(@this, nameof(@this));
            Asserts.ThrowIfNull(type, nameof(type));

            if (type.IsAssignableFrom(@this))
                return true;

            return InheritsOrImplements(@this, type);
        }

        //TODO: check
        private static bool InheritsOrImplements(this Type child, Type parent)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.IsGenericType
                ? child.GetGenericTypeDefinition()
                : child;

            while (currentChild != typeof(object))
            {
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                    return true;

                currentChild = currentChild.BaseType != null && currentChild.BaseType.IsGenericType
                    ? currentChild.BaseType.GetGenericTypeDefinition()
                    : currentChild.BaseType!;

                if (currentChild == null)
                    return false;
            }
            return false;
        }

        //TODO: check
        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces()
                .Any(childInterface =>
                {
                    var currentInterface = childInterface.IsGenericType
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
        }

        //TODO: check
        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            var shouldUseGenericType = !(parent.IsGenericType && parent.GetGenericTypeDefinition() != parent);

            if (parent.IsGenericType && shouldUseGenericType)
                parent = parent.GetGenericTypeDefinition();

            return parent;
        }
    }
}
