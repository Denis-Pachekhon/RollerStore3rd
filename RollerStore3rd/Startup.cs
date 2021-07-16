using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RollerStore.Data.Repositories.Abstract;
using RollerStore.Data.Repositories;
using RollerStore.Domain.Services.Abstract;
using RollerStore.Domain.Services;

namespace RollerStore3rd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RollerStore Api",
                    Description = ""
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });

            services.AddControllers();
            services.AddAutoMapper(typeof(Contracts.RollerProfile), typeof(Contracts.StoreProfile), typeof(RollerStore.Domain.StoreProfile), typeof(RollerStore.Domain.RollerProfile));

            string connString = Configuration.GetConnectionString("RollerStoreDB");
            services.AddDbContext<RollerStore.Data.DB.RollerStoreDbContext>(options =>
            {
                options.UseSqlServer(connString);
            });
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IRollerRepository, RollerRepository>();
            services.AddScoped<IRollerService, RollerService>();

            services.AddCors();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.
            AllowAnyOrigin().
            AllowAnyMethod().
            AllowAnyHeader());

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RollerStore API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
