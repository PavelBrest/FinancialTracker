using FinancialTracker.Core.Commons.Commands;

namespace FinancialTracker.Core.Tests.Stubs.Commands
{
    public class DecoratedCommandStub : ICommand
    {
        public bool IsDecoratorInvokes { get; set; }
    }
}