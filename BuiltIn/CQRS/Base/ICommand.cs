using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltIn.CQRS.Base
{
    public interface ICommand 
    {
    }

    public interface IQuery<Response>
    {
    }

    public interface ICommandHandler<T>
    where T : ICommand
    {
        void Handle(T command);
    }

    public interface IQueryHandler<T, Response>  
        where T : IQuery<Response>
    {
        Response Handle(T query);
    }


    public interface IRequestsBus
    {
        void Send<T>(T Command) where T : ICommand;
        TResponse Send<TResponse, TQuery>(TQuery query) where TQuery : IQuery<TResponse>;
    }
    public class RequestsBus : IRequestsBus
    {
        public RequestsBus(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlers = HttpContextAccessor.HttpContext.RequestServices
             .GetRequiredService<IEnumerable<ICommandHandler<TCommand>>>()
             .ToList();

            if (handlers.Count == 1)
            {
                handlers[0].Handle(command);
            }
            else if (handlers.Count == 0)
            {
                throw new System
                .Exception($"Command does not have any handler "
                            + $"{command.GetType().Name}");
            }
            else
            {
                throw new System
                .Exception($"Too many registred handlers - {handlers.Count}"
                            + $" for command {command.GetType().Name}");
            }
        }

        public TResponse Send<TResponse, TQuery>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var handlers = HttpContextAccessor.HttpContext.RequestServices
             .GetRequiredService<IEnumerable<IQueryHandler<TQuery, TResponse>>>()
             .ToList();
            if (handlers.Count == 1)
            {
                return handlers[0].Handle(query);
            }
            else if (handlers.Count == 0)
            {
                throw new System
                .Exception($"Command does not have any handler "
                            + $"{query.GetType().Name}");
            }
            else
            {
                throw new System
                .Exception($"Too many registred handlers - {handlers.Count}"
                            + $" for command {query.GetType().Name}");
            }
        }
    }
}
