using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Queries.Decorators;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Queries.AsyncQueries
{
    public class AsyncQueryHandlerStubDecorator : AsyncQueryHandlerDecorator<int, DecoratedQueryStub>
    {
        public AsyncQueryHandlerStubDecorator(IAsyncQueryHandler<int, DecoratedQueryStub> innerAsyncQueryHandler) : base(innerAsyncQueryHandler)
        {
        }

        public override Task<Result<int>> ExecuteAsync(DecoratedQueryStub query, CancellationToken cancellationToken = default)
        {
            query.IsDecoratorInvoked = true;
            return innerAsyncQueryHandler.ExecuteAsync(query, cancellationToken);
        }
    }
}