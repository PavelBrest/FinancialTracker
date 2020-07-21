using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons.Commands;

namespace FinancialTracker.Core.Commons.Buses
{
    public interface ICommandBus
    {
        Task<Result> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;

        Result Execute<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}