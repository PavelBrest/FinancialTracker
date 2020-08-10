using FinancialTracker.Core.Commons.Queries.Attributes;
using FinancialTracker.Core.Tests.Stubs.Queries;
using FinancialTracker.Core.Tests.Utils;
using System;

namespace FinancialTracker.Core.Tests.Commons.Queries.Attributes
{
    public class QueryDecoratorAttributeTests : DecoratorAttributeTestsBase<QueryDecoratorAttribute, QueryHandlerStub>
    {
        public override QueryDecoratorAttribute CreateDecorator(Type parameter)
        {
            return new QueryDecoratorAttribute(parameter);
        }
    }
}