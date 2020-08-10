using System;
using FinancialTracker.Core.Commons.Autofac;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Commons.Queries.Attributes
{
    public sealed class AsyncQueryDecoratorAttribute : Attribute
    {
        public Type DecoratorType { get; }

        public AsyncQueryDecoratorAttribute(Type decoratorType)
        {
            if (decoratorType == null) throw new ArgumentNullException(nameof(decoratorType));
            if (!decoratorType.IsAssignableTo(typeof(IAsyncQueryHandler<,>))) throw new InvalidOperationException("Decorator should be inherits from the IQueryHandler<>.");

            DecoratorType = decoratorType;
        }
    }
}