using System;
using FinancialTracker.Core.Commons.Autofac;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Commons.Queries.Attributes
{
    public sealed class QueryDecoratorAttribute : Attribute
    {
        public Type DecoratorType { get; }

        public QueryDecoratorAttribute(Type decoratorType)
        {
            if (decoratorType == null) throw new ArgumentNullException(nameof(decoratorType));
            if (!decoratorType.IsAssignableTo(typeof(IQueryHandler<>))) throw new InvalidOperationException("Decorator should be inherits from the IQueryHandler<>.");

            DecoratorType = decoratorType;
        }
    }
}
