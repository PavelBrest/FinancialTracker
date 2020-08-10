using System;
using System.Text;
using FinancialTracker.Core.Commons.Commands.Attributes;
using FinancialTracker.Core.Tests.Stubs.Commands.AsyncCommands;
using FinancialTracker.Core.Tests.Utils;

namespace FinancialTracker.Core.Tests.Commons.Commands.Attributes
{
    public class AsyncCommandDecoratorAttributeTests : DecoratorAttributeTestsBase<AsyncCommandDecoratorAttribute, AsyncCommandHandlerStub>
    {
        public override AsyncCommandDecoratorAttribute CreateDecorator(Type parameter)
        {
            return new AsyncCommandDecoratorAttribute(parameter);
        }
    }
}
