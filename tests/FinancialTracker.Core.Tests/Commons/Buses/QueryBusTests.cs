using Autofac;
using Autofac.Core;
using AutoFixture;
using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Buses.Impl;
using FinancialTracker.Core.Commons.Queries.Handlers;
using FinancialTracker.Core.Tests.Stubs;
using FinancialTracker.Core.Tests.Utils;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinancialTracker.Core.Tests.Commons.Buses
{
    public class QueryBusTests : AutofacTestsBase
    {
        private readonly Mock<IQueryHandler<int, QueryStub>> _queryHandler;
        private readonly Mock<IAsyncQueryHandler<int, QueryStub>> _asyncQueryHandler;

        private readonly QueryBus _underTests;
        private readonly Fixture _fixture;

        public QueryBusTests()
        {
            _queryHandler = new Mock<IQueryHandler<int, QueryStub>>();
            _asyncQueryHandler = new Mock<IAsyncQueryHandler<int, QueryStub>>();

            _containerBuilder.Register(factory => _queryHandler.Object).As<IQueryHandler<int, QueryStub>>();
            _containerBuilder.Register(factory => _asyncQueryHandler.Object).As<IAsyncQueryHandler<int, QueryStub>>();
            var lifetimeScope = _containerBuilder.Build().BeginLifetimeScope();

            _underTests = new QueryBus(lifetimeScope);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ExecuteAsync_When_Command_Null_Should_Throw_Exception()
        {
            QueryStub query = null;

            Func<Task> result = () => _underTests.ExecuteAsync<int, QueryStub>(query);

            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ExecuteAsync_When_CancellationRequested_Should_Return_Failed_With_OperationCanceledException()
        {
            var query = new QueryStub();
            var cancellationToken = new CancellationToken(true);

            var result = await _underTests.ExecuteAsync<int, QueryStub>(query, cancellationToken);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().BeOfType<OperationCanceledException>()
                .Which.CancellationToken.Should().Be(cancellationToken);
        }

        [Fact]
        public async Task ExecuteAsync_When_Type_Not_Registered_Should_Return_Failed_With_DependencyResolutionException()
        {
            var query = new QueryStub();
            var lifetimeScope = new ContainerBuilder().Build().BeginLifetimeScope();

            var result = await new QueryBus(lifetimeScope).ExecuteAsync<int, QueryStub>(query);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().BeOfType<DependencyResolutionException>()
                .Which.Message.Should().Contain(typeof(IAsyncQueryHandler<int, QueryStub>).FullName);
        }

        [Fact]
        public async Task ExecuteAsync_When_All_Ok_Should_Return_Success_Result()
        {
            var query = new QueryStub();
            var queryResult = _fixture.Create<int>();

            _asyncQueryHandler.Setup(p => p.ExecuteAsync(query, default)).ReturnsAsync(Result.Ok(queryResult));

            var result = await _underTests.ExecuteAsync<int, QueryStub>(query);

            _asyncQueryHandler.Verify(p => p.ExecuteAsync(query, default), Times.Once);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(queryResult);
        }

        [Fact]
        public void Execute_When_Command_Null_Should_Throw_Exception()
        {
            QueryStub query = null;

            Action result = () => _underTests.Execute<int, QueryStub>(query);

            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Execute_When_Type_Not_Registered_Should_Return_Failed_With_DependencyResolutionException()
        {
            var query = new QueryStub();
            var lifetimeScope = new ContainerBuilder().Build().BeginLifetimeScope();

            var result = new QueryBus(lifetimeScope).Execute<int, QueryStub>(query);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().BeOfType<DependencyResolutionException>()
                .Which.Message.Should().Contain(typeof(IQueryHandler<int, QueryStub>).FullName);
        }

        [Fact]
        public void Execute_When_All_Ok_Should_Return_Success_Result()
        {
            var query = new QueryStub();
            var queryResult = _fixture.Create<int>();

            _queryHandler.Setup(p => p.Execute(query)).Returns(Result.Ok(queryResult));

            var result = _underTests.Execute<int, QueryStub>(query);

            _queryHandler.Verify(p => p.Execute(query), Times.Once);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(queryResult);
        }
    }
}