using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Commands.Attributes;
using FinancialTracker.Core.Commons.Commands.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Commands.AsyncCommands
{
    [AsyncCommandDecorator(typeof(AsyncCommandHandlerStubDecorator))]
    public class DecoratedAsyncCommandHandlerStub : IAsyncCommandHandler<DecoratedCommandStub>
    {
        public Task<Result> ExecuteAsync(DecoratedCommandStub command, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok());
        }
    }
}