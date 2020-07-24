using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleMR.Behaviours;
using SampleMR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SampleMR.Application
{
    public static class MediateRSetup
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogExceptionBehavior<,>));
            services.AddSingleton(typeof(FakeCartService));

            return services;
        }
    }
}
