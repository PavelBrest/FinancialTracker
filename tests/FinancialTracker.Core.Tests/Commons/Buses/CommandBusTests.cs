using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using FinancialTracker.Core.Commons;
using FinancialTracker.Core.Commons.Buses.Impl;
using FinancialTracker.Core.Commons.Commands.Handlers;
using FinancialTracker.Core.Tests.Stubs;
using FinancialTracker.Core.Tests.Stubs.Commands;
using FinancialTracker.Core.Tests.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace FinancialTracker.Core.Tests.Commons.Buses
{
    public class CommandBusTests : AutofacTestsBase
    {
        private readonly Mock<ICommandHandler<CommandStub>> _commandHandler;
        private readonly Mock<IAsyncCommandHandler<CommandStub>> _asyncCommandHandler;

        private readonly CommandBus _underTests;

        public CommandBusTests()
        {
            _commandHandler = new Mock<ICommandHandler<CommandStub>>();
            _asyncCommandHandler = new Mock<IAsyncCommandHandler<CommandStub>>();

            _containerBuilder.Register(factory => _commandHandler.Object).As<ICommandHandler<CommandStub>>();
            _containerBuilder.Register(factory => _asyncCommandHandler.Object).As<IAsyncCommandHandler<CommandStub>>();
            var lifetimeScope = _containerBuilder.Build().BeginLifetimeScope();

            _underTests = new CommandBus(lifetimeScope);
        }

        [Fact]
        public async Task ExecuteAsync_When_Command_Null_Should_Throw_Exception()
        {
            CommandStub command = null;

            Func<Task> result = () => _underTests.ExecuteAsync(command);

            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ExecuteAsync_When_CancellationRequested_Should_Return_Failed_With_OperationCanceledException()
        {
            var command = new CommandStub();
            var cancellationToken = new CancellationToken(true);

            var result = await _underTests.ExecuteAsync(command, cancellationToken);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().BeOfType<OperationCanceledException>()
                .Which.CancellationToken.Should().Be(cancellationToken);
        }

        [Fact]
        public async Task ExecuteAsync_When_Type_Not_Registered_Should_Return_Failed_With_DependencyResolutionException()
        {
            var command = new CommandStub();
            var lifetimeScope = new ContainerBuilder().Build().BeginLifetimeScope();

            var result = await new CommandBus(lifetimeScope).ExecuteAsync(command);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().BeOfType<DependencyResolutionException>()
                .Which.Message.Should().Contain(typeof(IAsyncCommandHandler<CommandStub>).FullName);
        }

        [Fact]
        public async Task ExecuteAsync_When_All_Ok_Should_Return_Success_Result()
        {
            var command = new CommandStub();

            _asyncCommandHandler.Setup(p => p.ExecuteAsync(command, default)).ReturnsAsync(Result.Ok());

            var result = await _underTests.ExecuteAsync(command);

            _asyncCommandHandler.Verify(p => p.ExecuteAsync(command, default), Times.Once);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Execute_When_Command_Null_Should_Throw_Exception()
        {
            CommandStub command = null;

            Action result = () => _underTests.Execute(command);

            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Execute_When_Type_Not_Registered_Should_Return_Failed_With_DependencyResolutionException()
        {
            var command = new CommandStub();
            var lifetimeScope = new ContainerBuilder().Build().BeginLifetimeScope();

            var result = new CommandBus(lifetimeScope).Execute(command);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Exception.Should().BeOfType<DependencyResolutionException>()
                .Which.Message.Should().Contain(typeof(ICommandHandler<CommandStub>).FullName);
        }

        [Fact]
        public void Execute_When_All_Ok_Should_Return_Success_Result()
        {
            var command = new CommandStub();

            _commandHandler.Setup(p => p.Execute(command)).Returns(Result.Ok());

            var result = _underTests.Execute(command);

            _commandHandler.Verify(p => p.Execute(command), Times.Once);

            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }
    }
}
