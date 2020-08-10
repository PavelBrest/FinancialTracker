using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Commands.Decorators;
using FinancialTracker.Core.Commons.Commands.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Commands.AsyncCommands
{
    public class AsyncCommandHandlerStubDecorator : AsyncCommandHandlerDecorator<DecoratedCommandStub>
    {
        public AsyncCommandHandlerStubDecorator(IAsyncCommandHandler<DecoratedCommandStub> innerAsyncCommandHandler) : base(innerAsyncCommandHandler)
        {
        }

        public override Task<Result> ExecuteAsync(DecoratedCommandStub command, CancellationToken cancellationToken)
        {
            command.IsDecoratorInvokes = true;
            return InnerAsyncCommandHandler.ExecuteAsync(command, cancellationToken);
        }
    }
}