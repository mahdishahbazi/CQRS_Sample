using BuiltIn.CQRS.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace BuiltIn.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllCommandHandlers(this IServiceCollection services,
            Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var commandServiceType = typeof(ICommandHandler<>);
            var queryServiceType = typeof(IQueryHandler<,>);

            var typesFromAssemblies = assemblies
                .SelectMany(a => a.DefinedTypes.Where(t => t.GetInterfaces()
                .Any(x => x.IsGenericType && 
                    (x.GetGenericTypeDefinition() == commandServiceType ||
                    x.GetGenericTypeDefinition() == queryServiceType))));

            foreach (var type in typesFromAssemblies)
            {
                var allCommands = type.GetInterfaces()
                    .Where(x => x.IsGenericType && 
                    x.GetGenericTypeDefinition() == commandServiceType);
                foreach (var st in allCommands)
                {
                    services.Add(new ServiceDescriptor(st, type, lifetime));
                }

                var allQueries = type.GetInterfaces()
                    .Where(x => x.IsGenericType && 
                    x.GetGenericTypeDefinition() == queryServiceType);
                foreach (var st in allQueries)
                {
                    services.Add(new ServiceDescriptor(st, type, lifetime));
                }
            }


        }
    }
}
