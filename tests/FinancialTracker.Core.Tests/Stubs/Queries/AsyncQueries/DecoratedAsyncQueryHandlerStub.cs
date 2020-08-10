using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Queries.Attributes;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Queries.AsyncQueries
{
    [AsyncQueryDecorator(typeof(AsyncQueryHandlerStubDecorator))]
    public class DecoratedAsyncQueryHandlerStub : IAsyncQueryHandler<int, DecoratedQueryStub>
    {
        public Task<Result<int>> ExecuteAsync(DecoratedQueryStub query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok(0));
        }
    }
}