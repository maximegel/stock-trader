using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace StockTrader.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private static string Version => "v1";

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", $"StockTrader API {Version}"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Application:
            {
                services.AddPortfoliosApplication();
            }

            // Infrastructure:
            {
                services.AddPortfoliosPersistence(Configuration.GetSection("Portfolios"));
                services.AddInfrastructure();
            }

            // Api:
            {
                services
                    .AddControllers()
                    .AddPortfoliosControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    });

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(Version, new OpenApiInfo { Title = "StockTrader API", Version = Version });
                    options.EnableAnnotations();
                });
            }
        }
    }
}
