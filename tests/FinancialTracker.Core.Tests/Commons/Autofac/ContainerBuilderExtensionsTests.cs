using Autofac;
using FinancialTracker.Core.Commons.Autofac;
using FinancialTracker.Core.Commons.Commands.Handlers;
using FinancialTracker.Core.Commons.Queries.Handlers;
using FinancialTracker.Core.Tests.Stubs;
using FinancialTracker.Core.Tests.Stubs.Commands;
using FinancialTracker.Core.Tests.Stubs.Commands.AsyncCommands;
using FinancialTracker.Core.Tests.Stubs.Queries;
using FinancialTracker.Core.Tests.Stubs.Queries.AsyncQueries;
using FinancialTracker.Core.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace FinancialTracker.Core.Tests.Commons.Autofac
{
    public class ContainerBuilderExtensionsTests : AutofacTestsBase
    {
        [Fact]
        public void RegisterCommandHandler_When_No_Decorators_All_Ok_Should_Register_Command()
        {
            _containerBuilder.RegisterCommandHandler<CommandStub, CommandHandlerStub>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<ICommandHandler<CommandStub>>();

            result.Should().NotBeNull();
        }

        [Fact]
        public void RegisterCommandHandler_When_Decorator_All_Ok_Should_Register_Command()
        {
            var command = new DecoratedCommandStub();
            _containerBuilder.RegisterCommandHandler<DecoratedCommandStub, DecoratedCommandStubHandler>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<ICommandHandler<DecoratedCommandStub>>();
            result.Should().NotBeNull();

            result.Execute(command);
            command.IsDecoratorInvokes.Should().BeTrue();
        }

        [Fact]
        public void RegisterAsyncCommandHandler_When_No_Decorators_All_Ok_Should_Register_Command()
        {
            _containerBuilder.RegisterAsyncCommandHandler<CommandStub, AsyncCommandHandlerStub>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<IAsyncCommandHandler<CommandStub>>();

            result.Should().NotBeNull();
        }

        [Fact]
        public void RegisterCommandHandlerAsync_When_Decorator_All_Ok_Should_Register_Command()
        {
            var command = new DecoratedCommandStub();
            _containerBuilder.RegisterAsyncCommandHandler<DecoratedCommandStub, DecoratedAsyncCommandHandlerStub>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<IAsyncCommandHandler<DecoratedCommandStub>>();
            result.Should().NotBeNull();

            result.ExecuteAsync(command);
            command.IsDecoratorInvokes.Should().BeTrue();
        }

        [Fact]
        public void RegisterQueryHandler_When_No_Decorators_All_Ok_Should_Register_Query()
        {
            _containerBuilder.RegisterQueryHandler<int, QueryStub, QueryHandlerStub>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<IQueryHandler<int, QueryStub>>();
            result.Should().NotBeNull();
        }

        [Fact]
        public void RegisterQueryHandler_When_All_Ok_Should_Register_Query()
        {
            var query = new DecoratedQueryStub();
            _containerBuilder.RegisterQueryHandler<int, DecoratedQueryStub, DecoratedQueryHandlerStub>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<IQueryHandler<int, DecoratedQueryStub>>();
            result.Should().NotBeNull();

            result.Execute(query);
            query.IsDecoratorInvoked.Should().BeTrue();
        }

        [Fact]
        public void RegisterQueryHandlerAsync_When_No_Decorators_All_Ok_Should_Register_Query()
        {
            _containerBuilder.RegisterAsyncQueryHandler<int, QueryStub, AsyncQueryHandlerStub>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<IAsyncQueryHandler<int, QueryStub>>();
            result.Should().NotBeNull();
        }

        [Fact]
        public void RegisterQueryHandlerAsync_When_All_Ok_Should_Register_Query()
        {
            var query = new DecoratedQueryStub();
            _containerBuilder.RegisterAsyncQueryHandler<int, DecoratedQueryStub, DecoratedAsyncQueryHandlerStub>();

            var container = _containerBuilder.Build();

            var result = container.Resolve<IAsyncQueryHandler<int, DecoratedQueryStub>>();
            result.Should().NotBeNull();

            result.ExecuteAsync(query);
            query.IsDecoratorInvoked.Should().BeTrue();
        }
    }
}
