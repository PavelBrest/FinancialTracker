using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Queries.Decorators;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Queries
{
    public class QueryHandlerStubDecorator : QueryHandlerDecorator<int, DecoratedQueryStub>
    {
        public QueryHandlerStubDecorator(IQueryHandler<int, DecoratedQueryStub> innerQueryHandler) : base(innerQueryHandler)
        {
        }

        public override Result<int> Execute(DecoratedQueryStub query)
        {
            query.IsDecoratorInvoked = true;

            return innerQueryHandler.Execute(query);
        }
    }
}