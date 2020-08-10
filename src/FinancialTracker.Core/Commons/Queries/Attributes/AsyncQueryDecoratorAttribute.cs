using System;
using System.Reflection;
using FinancialTracker.Core.Commons.Autofac;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Commons.Queries.Attributes
{
    public sealed class AsyncQueryDecoratorAttribute : DecoratorAttribute
    {
        public AsyncQueryDecoratorAttribute(Type decoratorType) : base(decoratorType)
        {
            if (!decoratorType.IsAssignableTo(typeof(IAsyncQueryHandler<,>))) 
                throw new InvalidOperationException($"Decorator should be inherits from the {typeof(IAsyncQueryHandler<,>).FullName}.");
        }
    }
}