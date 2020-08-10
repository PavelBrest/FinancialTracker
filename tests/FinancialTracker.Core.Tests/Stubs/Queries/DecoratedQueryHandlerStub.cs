using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Queries.Attributes;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Queries
{
    [QueryDecorator(typeof(QueryHandlerStubDecorator))]
    public class DecoratedQueryHandlerStub : IQueryHandler<int, DecoratedQueryStub>
    {
        public Result<int> Execute(DecoratedQueryStub query)
        {
            return Result.Ok(0);
        }
    }
}