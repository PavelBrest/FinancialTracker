using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Queries.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Queries
{
    public class QueryHandlerStub : IQueryHandler<int, QueryStub>
    {
        public Result<int> Execute(QueryStub query)
        {
            return Result.Ok(0);
        }
    }
}
