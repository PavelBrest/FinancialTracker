using FinancialTracker.Core.Commons.Commands.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Commons.Commands.Decorators
{
    public abstract class AsyncCommandHandlerDecorator<TCommand> : IAsyncCommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected readonly IAsyncCommandHandler<TCommand> InnerAsyncCommandHandler;

        protected AsyncCommandHandlerDecorator(IAsyncCommandHandler<TCommand> innerAsyncCommandHandler)
        {
            InnerAsyncCommandHandler = innerAsyncCommandHandler ?? throw new ArgumentNullException(nameof(innerAsyncCommandHandler));
        }

        public abstract Task<Result> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
    }
}