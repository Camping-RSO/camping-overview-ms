using CampingOverviewAPI.Models;
using CampingOverviewAPI.Services;
using CampingOverviewAPI.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CampingOverviewAPI
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
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // get connection string from env var
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            // DB models service
            if (connectionString == null)
            {
                services.AddEntityFrameworkNpgsql().AddDbContext<avtokampiContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("Avtokampi"), o => o.SetPostgresVersion(new Version(9, 6, 13)))
                );
                connectionString = Configuration.GetConnectionString("Avtokampi");
            }
            else
            {
                services.AddEntityFrameworkNpgsql().AddDbContext<avtokampiContext>(options =>
                    options.UseNpgsql(connectionString, o => o.SetPostgresVersion(new Version(9, 6, 13)))
                );
            }

            // Repository services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAvtokampiRepository, AvtokampiRepository>();
            services.AddScoped<IKampirnaMestaRepository, KampirnaMestaRepository>();

            services.AddRouting(options => options.LowercaseUrls = true);

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Camping overview microservice API",
                    Version = "v1",
                    Description = "Web API for viewing data about camps."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // health checks
            services.AddHealthChecks().AddNpgSql(connectionString);

            services.AddHealthChecksUI(setupSettings: setup => setup.AddHealthCheckEndpoint("Database connection", "/health")).AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    var servers = new List<OpenApiServer>();

                    servers.Add(new OpenApiServer { Url = $"http://{httpReq.Host.Value}/camping-overview" });

                    swagger.Servers = servers;
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/camping-overview/swagger/v1/swagger.json", "kampi");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI();
            });
        }
    }
}
