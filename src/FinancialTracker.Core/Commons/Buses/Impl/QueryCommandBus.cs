using Autofac;
using FinancialTracker.Core.Commons.Queries;
using FinancialTracker.Core.Commons.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Commons.Buses.Impl
{
    internal class QueryBus : IQueryBus
    {
        private readonly ILifetimeScope _lifetimeScope;

        public QueryBus(ILifetimeScope lifetimeScope)
        {
            Asserts.ThrowIfNull(lifetimeScope, nameof(lifetimeScope));

            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public Task<Result<TResult>> ExecuteAsync<TResult, TQuery>(TQuery query, CancellationToken cancellationToken = default) 
            where TQuery : class, IQuery<TResult>
        {
            Asserts.ThrowIfNull(query, nameof(query));

            if (Asserts.IsCancellationRequested(cancellationToken, out var result))
                return Task.FromResult(Result.Wrap(result, default(TResult)!));

            if (!Asserts.IsTypeRegistered(_lifetimeScope, typeof(IAsyncQueryHandler<TResult, TQuery>), out result))
                return Task.FromResult(Result.Wrap(result, default(TResult)!));

            var instance = _lifetimeScope.Resolve<IAsyncQueryHandler<TResult, TQuery>>();
            return instance.ExecuteAsync(query, cancellationToken);
        }

        public Result<TResult> Execute<TResult, TQuery>(TQuery query) 
            where TQuery : class, IQuery<TResult>
        {
            Asserts.ThrowIfNull(query, nameof(query));

            if (!Asserts.IsTypeRegistered(_lifetimeScope, typeof(IQueryHandler<TResult, TQuery>), out var result))
                return Result.Wrap(result, default(TResult)!);

            var instance = _lifetimeScope.Resolve<IQueryHandler<TResult, TQuery>>();
            return instance.Execute(query);
        }
    }
}
