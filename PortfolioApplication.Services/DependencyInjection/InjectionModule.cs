﻿using Autofac;
using PortfolioApplication.Services.CQRS.Commands;
using PortfolioApplication.Services.CQRS.Queries;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Linq;
using System.Reflection;

namespace PortfolioApplication.Services.DependencyInjection
{
    public class InjectionModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterQueries(builder);
            RegisterDatabaseSet(builder);
            RegisterCommands(builder);
            RegisterUnitOfWork(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(x => x.IsAssignableTo<IHandleCommand>())
            .AsImplementedInterfaces();

            builder.Register(RegisterHandlersFactoryDelegate());

            builder.RegisterType<CommandBus>()
                .AsImplementedInterfaces();
        }

        private Func<IComponentContext, Func<Type, IHandleCommand>> RegisterHandlersFactoryDelegate()
        {
            return c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return ResolveCommand(ctx);
            };
        }

        private Func<Type, IHandleCommand> ResolveCommand(IComponentContext ctx)
        {
            return t =>
            {
                var handlerType = typeof(IHandleCommand<>).MakeGenericType(t);

                return (IHandleCommand)ctx.Resolve(handlerType);
            };
        }

        private void RegisterQueries(ContainerBuilder builder)
        {
            var repositoryAssembly = typeof(TechnologyTypeEntityQuery).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(repositoryAssembly)
                .Where(t => t.Name.EndsWith("Query"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void RegisterDatabaseSet(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseSet>()
                .As<IDatabaseSet>()
                .InstancePerLifetimeScope();
        }

        private void RegisterUnitOfWork(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
