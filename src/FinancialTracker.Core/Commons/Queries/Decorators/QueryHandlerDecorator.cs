using System;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Commons.Queries.Decorators
{
    public abstract class QueryHandlerDecorator<TReturn, TQuery> : IQueryHandler<TReturn, TQuery>
        where TQuery : IQuery<TReturn>
    {
        protected readonly IQueryHandler<TReturn, TQuery> innerQueryHandler;

        protected QueryHandlerDecorator(IQueryHandler<TReturn, TQuery> innerQueryHandler)
        {
            this.innerQueryHandler = innerQueryHandler ?? throw new ArgumentNullException(nameof(innerQueryHandler));
        }

        public abstract Result<TReturn> Execute(TQuery query);
    }
}
