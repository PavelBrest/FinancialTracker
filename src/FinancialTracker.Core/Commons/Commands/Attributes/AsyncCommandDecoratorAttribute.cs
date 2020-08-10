using FinancialTracker.Core.Commons.Autofac;
using FinancialTracker.Core.Commons.Commands.Handlers;
using System;

namespace FinancialTracker.Core.Commons.Commands.Attributes
{
    public sealed class AsyncCommandDecoratorAttribute : DecoratorAttribute
    {
        public AsyncCommandDecoratorAttribute(Type decoratorType) : base(decoratorType)
        {
            if (!decoratorType.IsAssignableTo(typeof(IAsyncCommandHandler<>))) 
                throw new InvalidOperationException($"Decorator should be inherits from the {typeof(IAsyncCommandHandler<>).FullName}.");
        }
    }
}