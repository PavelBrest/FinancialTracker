using System.Threading;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Commons.Commands.Handlers
{
    public interface IAsyncCommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<Result> ExecuteAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}