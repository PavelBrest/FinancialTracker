using Autofac;
using FinancialTracker.Core.Commons.Commands;
using FinancialTracker.Core.Commons.Commands.Handlers;
using System.Threading;
using System.Threading.Tasks;
using FinancialTracker.Core.Commons.Utils;

namespace FinancialTracker.Core.Commons.Buses.Impl
{
    internal class CommandBus : ICommandBus
    {
        private readonly ILifetimeScope _lifetimeScope;

        public CommandBus(ILifetimeScope lifetimeScope)
        {
            Asserts.ThrowIfNull(lifetimeScope, nameof(lifetimeScope));

            _lifetimeScope = lifetimeScope;
        }

        public Task<Result> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class, ICommand
        {
            Asserts.ThrowIfNull(command, nameof(command));

            if (Asserts.IsCancellationRequested(cancellationToken, out var result))
                return Task.FromResult(result);

            if (!Asserts.IsTypeRegistered(_lifetimeScope, typeof(IAsyncCommandHandler<TCommand>), out result))
                return Task.FromResult(result);

            var instance = _lifetimeScope.Resolve<IAsyncCommandHandler<TCommand>>();
            return instance.ExecuteAsync(command, cancellationToken);
        }

        public Result Execute<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            Asserts.ThrowIfNull(command, nameof(command));

            if (!Asserts.IsTypeRegistered(_lifetimeScope, typeof(ICommandHandler<TCommand>), out var result))
                return result;

            var instance = _lifetimeScope.Resolve<ICommandHandler<TCommand>>();
            return instance.Execute(command);
        }
    }
}
