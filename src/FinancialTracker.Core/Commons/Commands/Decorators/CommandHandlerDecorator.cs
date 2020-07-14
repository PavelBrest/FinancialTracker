using System;
using FinancialTracker.Core.Commons.Commands.Handlers;

namespace FinancialTracker.Core.Commons.Commands.Decorators
{
    public abstract class CommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected readonly ICommandHandler<TCommand> InnerCommandHandler;

        protected CommandHandlerDecorator(ICommandHandler<TCommand> innerCommandHandler)
        {
            InnerCommandHandler = innerCommandHandler ?? throw new ArgumentNullException(nameof(innerCommandHandler));
        }

        public abstract Result Execute(TCommand command);
    }
}
