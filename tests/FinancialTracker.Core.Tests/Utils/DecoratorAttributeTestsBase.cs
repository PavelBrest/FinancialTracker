using System;
using System.Collections.Generic;
using FinancialTracker.Core.Commons;
using FluentAssertions;
using Xunit;

namespace FinancialTracker.Core.Tests.Utils
{
    public abstract class DecoratorAttributeTestsBase<TAttribute, TValidDecoratedType>
    {
        [Fact]
        public virtual void Ctor_When_DecoratorType_Null_Should_Throw_ArgumentNullException()
        {
            Action result = () => CreateDecorator(null);

            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public virtual void Ctor_When_DecoratorType_Is_Not_Valid_Type_Should_Throw_InvalidOperationException()
        {
            Action result = () => CreateDecorator(typeof(ICollection<>));

            result.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public virtual void Ctor_When_All_Ok_Should_Set_Type()
        {
            var result = CreateDecorator(typeof(TValidDecoratedType));

            result.Should().BeAssignableTo<DecoratorAttribute>()
                .Which.DecoratorType.Should().Be(typeof(TValidDecoratedType));
        }

        public abstract TAttribute CreateDecorator(Type parameter);
    }
}