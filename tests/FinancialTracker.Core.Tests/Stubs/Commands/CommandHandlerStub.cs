using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Commands.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Commands
{
    public class CommandHandlerStub : ICommandHandler<CommandStub>
    {
        public Result Execute(CommandStub command)
        {
            return Result.Ok();
        }
    }
}