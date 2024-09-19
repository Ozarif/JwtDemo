using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JwtDemo.Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace JwtDemo.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
         public static IServiceCollection AddApplicationServices(this IServiceCollection services)
         {
            			services.AddMediatR(configuration =>
			{
				configuration.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
				configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

			});

			services.AddValidatorsFromAssembly(typeof(ApplicationDependencyInjection).Assembly, includeInternalTypes: true);

			return services;
         }
		 
    }
}