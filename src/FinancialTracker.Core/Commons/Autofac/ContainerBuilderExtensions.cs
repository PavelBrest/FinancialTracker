using Autofac;
using FinancialTracker.Core.Commons.Commands;
using FinancialTracker.Core.Commons.Commands.Handlers;
using FinancialTracker.Core.Commons.Queries;
using FinancialTracker.Core.Commons.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialTracker.Core.Commons.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterCommandHandler<TCommand, TCommandHandler>(this ContainerBuilder builder)
            where TCommandHandler : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            builder.RegisterType<TCommandHandler>().As<ICommandHandler<TCommand>>();
            builder.RegisterDecorators<TCommandHandler, ICommandHandler<TCommand>>();
        }

        public static void RegisterAsyncCommandHandler<TCommand, TAsyncCommandHandler>(this ContainerBuilder builder)
            where TAsyncCommandHandler : IAsyncCommandHandler<TCommand>
            where TCommand : ICommand
        {
            builder.RegisterType<TAsyncCommandHandler>().As<IAsyncCommandHandler<TCommand>>();
            builder.RegisterDecorators<TAsyncCommandHandler, IAsyncCommandHandler<TCommand>>();
        }

        public static void RegisterQueryHandler<TReturn, TQuery, TQueryHandler>(this ContainerBuilder builder)
            where TQuery : IQuery<TReturn>
            where TQueryHandler : IQueryHandler<TReturn, TQuery>
        {
            builder.RegisterType<TQueryHandler>().As<IQueryHandler<TReturn, TQuery>>();
            builder.RegisterDecorators<TQueryHandler, IQueryHandler<TReturn, TQuery>>();
        }

        public static void RegisterAsyncQueryHandler<TReturn, TQuery, TAsyncQueryHandler>(this ContainerBuilder builder)
            where TQuery : IQuery<TReturn>
            where TAsyncQueryHandler : IAsyncQueryHandler<TReturn, TQuery>
        {
            builder.RegisterType<TAsyncQueryHandler>().As<IAsyncQueryHandler<TReturn, TQuery>>();
            builder.RegisterDecorators<TAsyncQueryHandler, IAsyncQueryHandler<TReturn, TQuery>>();
        }

        internal static void RegisterDecorators<THandler, TInterface>(this ContainerBuilder builder)
        {
            var decorators = GetDecoratorsType<THandler>();

            foreach (var decoratorAttribute in decorators)
                builder.RegisterDecorator(decoratorAttribute, typeof(TInterface), c => c.ImplementationType == typeof(THandler));
        }

        internal static IEnumerable<Type> GetDecoratorsType<THandler>()
        {
            return typeof(THandler).GetCustomAttributes(false)
                .Where(p => p.GetType().IsAssignableTo<DecoratorAttribute>())
                .Cast<DecoratorAttribute>()
                .Select(p => p.DecoratorType);
        }
    }
}
