using System;
using FinancialTracker.Core.Commons.Commands.Attributes;
using FinancialTracker.Core.Tests.Stubs.Commands;
using FinancialTracker.Core.Tests.Utils;

namespace FinancialTracker.Core.Tests.Commons.Commands.Attributes
{
    public class CommandDecoratorAttributeTests : DecoratorAttributeTestsBase<CommandDecoratorAttribute, CommandHandlerStub>
    {
        public override CommandDecoratorAttribute CreateDecorator(Type parameter)
        {
            return new CommandDecoratorAttribute(parameter);
        }
    }
}