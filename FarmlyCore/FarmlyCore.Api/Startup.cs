using AutoMapper;
using FarmlyCore.Application.MapperProfile;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FarmlyCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FarmlyCore.Api", Version = "v1" });
            });

            //services.RegisterApplicationQueryHandlers();            

            //var mssqlConnectionString = Configuration.GetConnectionString("EMG_MSSQL");

            //services.AddDbContextPool<FarmlyEntityDbContext>(
            //    dbContextOptions => dbContextOptions
            //        .UseSqlServer(
            //            mssqlConnectionString
            //));

            var mapperConfig = new MapperConfiguration(cfg =>
                  cfg.AddMaps(new[]
                  {
                      typeof(CustomerProfile),
                      typeof(OrderProfile),
                      typeof(AdvertProfile),
                      typeof(UserProfile)
                  })
            );

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FarmlyCore.Api v1"));
            }

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
