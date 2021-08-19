using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleCqrs.Common.Application;
using SimpleCqrs.Common.Persistence;
using SimpleCqrs.Portfolios.Domain;
using SimpleCqrs.Portfolios.Persistence;
using SimpleCqrs.Portfolios.Persistence.DataModels;

namespace SimpleCqrs
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private IConfiguration Configuration { get; }
        private static string Version => "v1";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Persistence:
            services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IWriteOnlyRepository<Portfolio>>(provider =>
                new WriteOnlyMappingRepository<Portfolio, PortfolioData>(
                    new DbWriteOnlyRepository<PortfolioData>(provider.GetRequiredService<PortfolioDbContext>()),
                    provider.GetRequiredService<IMapper>()));

            // Application:
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));
            services.AddScoped<ICommandBus, InMemoryCommandBus>();

            // Api:
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new OpenApiInfo {Title = "SimpleCqrs.Api", Version = Version});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"SimpleCqrs.Api {Version}"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}