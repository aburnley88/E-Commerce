using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this WebApplicationBuilder builder)
        {                      
            builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
            builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var erros = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x =>x.ErrorMessage).ToArray();
        
                    var errorResponse = new ApiValidationResponse
                    {
                        Errors = erros
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return builder.Services; 
        }
    }
}