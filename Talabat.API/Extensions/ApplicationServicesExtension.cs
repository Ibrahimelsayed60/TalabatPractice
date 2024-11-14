using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped(typeof(IBasketRepository),typeof( BasketRepository));

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService, OrderService>();    
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            Services.AddAutoMapper(typeof(MappingProfiles));

            Services.AddScoped<IPaymentService, PaymentService>();

            #region Error - Handling
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    // ModelState => Dictionary [KeyValue pair]
                    // Key => Name of Param
                    // Value => Error

                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                .SelectMany(P => P.Value.Errors)
                                                .Select(E => E.ErrorMessage)
                                                .ToArray();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            #endregion

            return Services;
        }
    }
}
