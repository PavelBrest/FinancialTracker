using FinancialTracker.Core.Commons.Autofac;
using FinancialTracker.Core.Commons.Commands.Handlers;
using System;

namespace FinancialTracker.Core.Commons.Commands.Attributes
{
    public sealed class CommandDecoratorAttribute : DecoratorAttribute
    {
        public CommandDecoratorAttribute(Type decoratorType) : base(decoratorType)
        {
            if (!decoratorType.IsAssignableTo(typeof(ICommandHandler<>))) 
                throw new InvalidOperationException("Decorator should be inherits from the ICommandHandler<>.");
        }
    }
}
