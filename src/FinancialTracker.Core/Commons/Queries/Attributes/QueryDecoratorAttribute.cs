using System;
using FinancialTracker.Core.Commons.Autofac;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Commons.Queries.Attributes
{
    public sealed class QueryDecoratorAttribute : DecoratorAttribute
    {
        public QueryDecoratorAttribute(Type decoratorType) : base(decoratorType)
        {
            if (!decoratorType.IsAssignableTo(typeof(IQueryHandler<,>))) 
                throw new InvalidOperationException($"Decorator should be inherits from the {typeof(IQueryHandler<,>).FullName}.");
        }
    }
}
