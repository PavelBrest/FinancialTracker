using FinancialTracker.Core.Commons.Queries.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Commons.Queries.Decorators
{
    public abstract class AsyncQueryHandlerDecorator<TReturn, TQuery> : IAsyncQueryHandler<TReturn, TQuery>
        where TQuery : IQuery<TReturn>
    {
        protected readonly IAsyncQueryHandler<TReturn, TQuery> innerAsyncQueryHandler;

        protected AsyncQueryHandlerDecorator(IAsyncQueryHandler<TReturn, TQuery> innerAsyncQueryHandler)
        {
            this.innerAsyncQueryHandler = innerAsyncQueryHandler ?? throw new ArgumentNullException(nameof(innerAsyncQueryHandler));
        }

        public abstract Task<Result<TReturn>> ExecuteAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}