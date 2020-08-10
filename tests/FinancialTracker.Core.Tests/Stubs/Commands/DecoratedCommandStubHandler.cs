using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Commands.Attributes;
using FinancialTracker.Core.Commons.Commands.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Commands
{
    [CommandDecorator(typeof(CommandHandlerStubDecorator))]
    public class DecoratedCommandStubHandler : ICommandHandler<DecoratedCommandStub>
    {
        public Result Execute(DecoratedCommandStub command)
        {
            return Result.Ok();
        }
    }
}