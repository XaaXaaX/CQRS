using CQRS.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Application.CommandBus
{
    /// <summary>
    /// This is an custom generic implementation of StructureMap Pattern 
    /// 
    /// </summary>
    public sealed class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;
        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public ICommandHandler<T> GetHandler<T>() where T : class, ICommand
        {
            List<Type> handlers = GetHandlerTypes<T>().ToList();

            Type runtimeHandlertype = handlers.Select(handel
                => handel).FirstOrDefault();

            var typeToResolve = runtimeHandlertype.GetInterfaces()
                                                .Where(i => !i.IsGenericType)
                                                .FirstOrDefault();
            var handler = serviceProvider.GetService(typeToResolve);

            return handler as ICommandHandler<T>;
        }

        private IEnumerable<Type> GetHandlerTypes<T>() where T : class, ICommand
        {
            var handlers = typeof(ICommandHandler<>).Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                    .Where(h => h.GetInterfaces()
                        .Any(ii => ii.GetGenericArguments()
                            .Any(aa => aa == typeof(T)))).ToList();


            return handlers;
        }

    }

}
