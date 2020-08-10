using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Queries.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Tests.Stubs.Queries.AsyncQueries
{
    public class AsyncQueryHandlerStub : IAsyncQueryHandler<int, QueryStub>
    {
        public Task<Result<int>> ExecuteAsync(QueryStub query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok(0));
        }
    }
}
