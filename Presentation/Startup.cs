using Application.Services;
using Application.Services.Abstractions;
using Data.Repository;
using Data.Repository.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

namespace Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ledger API v1");
                c.RoutePrefix = string.Empty; 
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureControllers(services);
            ConfigureSwagger(services);
            RegisterServices(services);
        }

        private static void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers();
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ledger API",
                    Version = "v1"
                });
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<ILedgerService, LedgerService>();
        }        
    }
}
