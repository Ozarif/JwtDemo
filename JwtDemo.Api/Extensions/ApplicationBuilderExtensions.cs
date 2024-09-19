using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtDemo.Api.Middlewares;

namespace JwtDemo.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
            public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
    }
}