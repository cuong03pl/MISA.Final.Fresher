using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using MISA.Infrastructure.Repositories;
using MISA.QLTS.Core.Interfaces.Repository;
using MISA.QLTS.Core.Interfaces.Service;
using MISA.QLTS.Core.Services;
using MISA.QLTS.Infrastructure.Repositories;

namespace MISA.QLTS.Fresher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Add controllers
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // DI Services
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IAssetRepo, AssetRepo>();
            services.AddScoped<IAssetTypeRepo, AssetTypeRepo>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetTypeService, AssetTypeService>();
            services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
        }

        
        // middleware 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ValidateExceptionMiddleware>();
            app.UseCors("AllowOrigin");
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
