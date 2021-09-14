using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StockTrader.Common.Application.Messaging;
using StockTrader.Common.Application.Persistence;
using StockTrader.Common.Infrastructure.Messaging.MediatR;
using StockTrader.Portfolios.Domain;
using StockTrader.Portfolios.Persistence;

namespace StockTrader
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private static string Version => "v1";
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Application:
            
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddScoped<ICommandBus, MediatorCommandBus>();
            
            // Infrastructure:
            
            services.AddScoped<IRepository<IPortfolio>, PortfolioDbRepository>();
            
            services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            // Api:
            
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
            services.AddSwaggerGen(options =>
            {
                options.CustomOperationIds(dsc => ((ControllerActionDescriptor)dsc.ActionDescriptor).ActionName);
                options.SwaggerDoc(Version, new OpenApiInfo {Title = "StockTrader.Api", Version = Version});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => 
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", $"StockTrader.Api {Version}"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}