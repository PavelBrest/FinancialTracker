using Autofac;
using Autofac.Core;
using FinancialTracker.Core.Commons.Commands;
using FinancialTracker.Core.Commons.Commands.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialTracker.Core.Commons.Buses.Impl
{
    internal class CommandBus : ICommandBus
    {
        private readonly ILifetimeScope _lifetimeScope;

        public CommandBus(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
        }

        public Task<Result> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            if (cancellationToken.IsCancellationRequested)
                return Task.FromResult(Result.Failed("Command was cancelled."));

            using var scope = _lifetimeScope.BeginLifetimeScope();

            if (!scope.TryResolve(typeof(IAsyncCommandHandler<TCommand>), out var instance))
                throw new DependencyResolutionException($"IAsyncCommandHandler<{typeof(TCommand).FullName}> not registered");

            return ((IAsyncCommandHandler<TCommand>) instance).ExecuteAsync(command, cancellationToken);
        }

        public Result Execute<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            using var scope = _lifetimeScope.BeginLifetimeScope();

            if (!scope.TryResolve(typeof(IAsyncCommandHandler<TCommand>), out var instance))
                throw new DependencyResolutionException($"ICommandHandler<{typeof(TCommand).FullName}> not registered");

            return ((ICommandHandler<TCommand>) instance).Execute(command);
        }
    }
}
