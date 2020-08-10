using FinancialTracker.Core.Commons.Queries;

namespace FinancialTracker.Core.Tests.Stubs.Queries
{
    public class DecoratedQueryStub : IQuery<int>
    {
        public bool IsDecoratorInvoked { get; set; }
    }
}