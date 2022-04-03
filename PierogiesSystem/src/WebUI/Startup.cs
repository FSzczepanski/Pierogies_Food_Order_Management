using CleanArchitecture.Application.Mails.models;
using MailKit;

namespace CleanArchitecture.WebUI
{
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
    using Domain.Entities;
    using Extensions;
    using Helpers;
    using Infrastructure.Services;
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
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IEmailService, EmailService>();
            
            services.AddHttpContextAccessor();
            
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new StringConverterHelper());
                    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeToStringConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeNullToStringConverter());
                });
            
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        
            services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            
            services.AddLogging();
            services.AddMvc();
         
            
            services.Configure<ApiBehaviorOptions>(options => 
                options.SuppressModelStateInvalidFilter = true);

            services.AddSwaggerDocumentation();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
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
            
            /*app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }*/
            
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
                options.WithOrigins(
                        "http://localhost:8080", "https://localhost:8080",
                        "http://localhost:8081", "https://localhost:8081",
                        "https://localhost:44312",
                        "https://68.183.214.2:8080", "http://68.183.214.2:8080",
                        "http://zamowienia.staryfolwark.com.pl",
                        "http://zamowienia.staryfolwark.com.pl:8080", "https://zamowienia.staryfolwark.com.pl:8080/",
                        "http://68.183.214.2",
                        "https://68.183.214.2:5432", "http://68.183.214.2:5432") 
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
