using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Vrnz2.BaseInfra.Assemblies;
using Vrnz2.BaseInfra.Logs;
using Vrnz2.BaseInfra.ServiceCollection;
using Vrnz2.BaseWebApi.Helpers;
using Vrnz2.CypressTest.Api.Helpers.Extensions;

namespace Vrnz2.CypressTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                    .AddScoped<ControllerHelper>()
                    .AddLogs()
                    .AddMediatR(AssembliesHelper.GetAssemblies<Program>())
                    .AddIServiceColletion()
                    .AddValidations()
                    .AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vrnz2.CypressTest.Api", Version = "v1" }))
                    .AddControllers()
                    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app
                    .UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vrnz2.CypressTest.Api v1"));

            app
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
