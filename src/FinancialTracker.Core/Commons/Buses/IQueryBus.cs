using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons.Queries;

namespace FinancialTracker.Core.Commons.Buses
{
    public interface IQueryBus
    {
        Task<Result<TResult>> ExecuteAsync<TResult, TQuery>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : class, IQuery<TResult>;

        Result<TResult> Execute<TResult, TQuery>(TQuery query)
            where TQuery : class, IQuery<TResult>;
    }
}