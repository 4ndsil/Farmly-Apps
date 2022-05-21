using AutoMapper;
using FarmlyCore.Api.OperationFilters;
using FarmlyCore.Application.Extensions;
using FarmlyCore.Application.MapperProfile;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Net.Mime;

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
            services.AddControllers(opt => opt.Filters.Add<OperationCancelledExceptionFilter>())
               .AddNewtonsoftJson(opt => opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FarmlyCore.Api", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.RegisterApplicationQueryHandlers();

            var mssqlConnectionString = Configuration.GetConnectionString("Farmly_MSSQL");

            services.AddDbContextPool<FarmlyEntityDbContext>(
                dbContextOptions => dbContextOptions
                    .UseSqlServer(
                        mssqlConnectionString
            ));

            var mapperConfig = new MapperConfiguration(cfg =>
                  cfg.AddMaps(new[]
                  {
                      typeof(CustomerProfile),
                      typeof(CustomerAddressProfile),
                      typeof(OrderProfile),
                      typeof(AdvertProfile),
                      typeof(UserProfile),
                      typeof(CategoryProfile)
                  })
            );

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddSwaggerDocument();

            //Log invalid model state
            services.PostConfigure<ApiBehaviorOptions>(options =>
            {
                var builtInFactory = options.InvalidModelStateResponseFactory;

                options.InvalidModelStateResponseFactory = context =>
                {
                    var loggerFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ILoggerFactory>();

                    var logger = loggerFactory.CreateLogger(context.ActionDescriptor.DisplayName);

                    var errors = context.ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage);

                    logger?.LogWarning($"ModelState Invalid:'{string.Join("; ", errors)}'");

                    return builtInFactory(context);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();                  
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    var exception = exceptionHandlerPathFeature.Error;

                    var lf = app.ApplicationServices.GetService<ILoggerFactory>();

                    var logger = lf.CreateLogger("ExceptionHandlerLogger");

                    logger.LogCritical(exception.Message, exception);

                    var result = JsonConvert.SerializeObject(new ProblemDetails
                    {
                        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                        Status = StatusCodes.Status500InternalServerError,
                        Title = exception?.Message,
                        Detail = exception?.InnerException?.ToString(),
                        Instance = context?.Request?.Path
                    });

                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    await context.Response.WriteAsync(result);
                }));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}