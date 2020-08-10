using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Commands.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Commands.AsyncCommands
{
    public class AsyncCommandHandlerStub : IAsyncCommandHandler<CommandStub>
    {
        public Task<Result> ExecuteAsync(CommandStub command, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok());
        }
    }
}