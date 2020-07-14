using System.Threading;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Commons.Queries.Handlers
{
    public interface IAsyncQueryHandler<TReturn, in TQuery>
        where TQuery : IQuery<TReturn>
    {
        Task<Result<TReturn>> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}