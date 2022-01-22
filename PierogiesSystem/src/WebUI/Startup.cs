using CleanArchitecture.Application;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.WebUI.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.WebUI
{
    using Domain.Entities;
    using Extensions;
    using Microsoft.OpenApi.Models;
    using Middleware;

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
            
            
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pierogies.WebApi", Version = "v1" });
            });
            
            services.AddCors();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpContextAccessor();
            
            
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        
            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            
            services.AddLogging();
            services.AddMvc();
         
            
            services.Configure<ApiBehaviorOptions>(options => 
                options.SuppressModelStateInvalidFilter = true);

            services.AddSwaggerDocumentation();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error"); 
                app.UseHsts();
            }
            
            

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });
            
            app.UseRouting();
            // app.UseAuthentication();
            //
            // app.UseAuthorization();
            //
            app.UseCors(options =>
                options.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            
            app.UseMiddleware<JwtMiddleware>();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            
            app.UseSwaggerDocumentation();
        }
    }
}