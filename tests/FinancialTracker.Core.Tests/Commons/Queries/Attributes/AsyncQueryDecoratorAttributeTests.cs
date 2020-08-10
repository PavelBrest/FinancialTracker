using System;
using FinancialTracker.Core.Commons.Queries.Attributes;
using FinancialTracker.Core.Tests.Stubs.Queries.AsyncQueries;
using FinancialTracker.Core.Tests.Utils;

namespace FinancialTracker.Core.Tests.Commons.Queries.Attributes
{
    public class AsyncQueryDecoratorAttributeTests : DecoratorAttributeTestsBase<AsyncQueryDecoratorAttribute, AsyncQueryHandlerStub>
    {
        public override AsyncQueryDecoratorAttribute CreateDecorator(Type parameter)
        {
            return new AsyncQueryDecoratorAttribute(parameter);
        }
    }
}