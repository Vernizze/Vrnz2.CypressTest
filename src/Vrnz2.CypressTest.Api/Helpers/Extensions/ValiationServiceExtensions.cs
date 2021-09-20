using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Vrnz2.BaseInfra.Validations;
using AddProduct = Vrnz2.CypressTest.Api.Services.AddProduct.AddProduct.Model;
using GetProduct = Vrnz2.CypressTest.Api.Services.GetProduct.GetProduct.Model;

namespace Vrnz2.CypressTest.Api.Helpers.Extensions
{
    public static class ValiationServiceExtensions
    {
        public static IServiceCollection AddValidations(this IServiceCollection services)
            => services
                .AddScoped<IValidatorFactory, ValidatorFactory>()
                .AddScoped<ValidationHelper>()
                .AddTransient<IValidator<GetProduct.Input>, GetProduct.RequestValidator>()
                .AddTransient<IValidator<AddProduct.Input>, AddProduct.RequestValidator>()
                ;
    }
}
