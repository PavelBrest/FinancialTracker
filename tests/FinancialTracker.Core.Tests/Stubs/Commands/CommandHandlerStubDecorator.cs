using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Commands.Decorators;
using FinancialTracker.Core.Commons.Commands.Handlers;

namespace FinancialTracker.Core.Tests.Stubs.Commands
{
    public class CommandHandlerStubDecorator : CommandHandlerDecorator<DecoratedCommandStub>
    {
        public CommandHandlerStubDecorator(ICommandHandler<DecoratedCommandStub> innerCommandHandler) : base(innerCommandHandler)
        {
        }

        public override Result Execute(DecoratedCommandStub command)
        {
            command.IsDecoratorInvokes = true;

            return _innerCommandHandler.Execute(command);
        }
    }
}