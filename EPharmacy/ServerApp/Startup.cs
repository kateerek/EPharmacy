using System;
using System.Globalization;
using System.Text;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Entities;
using EPharmacy.Data.Entities.Users;
using EPharmacy.ServerApp.Common;
using EPharmacy.ServerApp.Filters.ExceptionFilter;
using EPharmacy.ServerApp.Services.Account;
using EPharmacy.ServerApp.Services.ActiveSubstance;
using EPharmacy.ServerApp.Services.Attributes;
using EPharmacy.ServerApp.Services.BusinessIntelligence;
using EPharmacy.ServerApp.Services.Discount;
using EPharmacy.ServerApp.Services.MailSender;
using EPharmacy.ServerApp.Services.Pharmacy;
using EPharmacy.ServerApp.Services.Prescription;
using EPharmacy.ServerApp.Services.Producer;
using EPharmacy.ServerApp.Services.Products;
using EPharmacy.ServerApp.Services.SalesOrder;
using EPharmacy.ServerApp.Services.Storage;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;

namespace EPharmacy.ServerApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private ILogger<Startup> Logger { get; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Logger = logger;
            Configuration = configuration;            
        }


        public void ConfigureServices(IServiceCollection services)
        {
            Logger.LogInformation("Start configuring services");            
            LoadOptions(services);
            ConfigureMvcService(services);
            ConfigureDbContextService(services);
            ConfigureAutoMapperService(services);
            ConfigureAuthenticationService(services);
            ConfigureSwaggerService(services);
            ConfigureSpaService(services);
            ConfigureCustomServices(services);
            ConfigureAzureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {            
            Logger.LogInformation("Start configuring App");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseAuthentication();
            ConfigureRequestLocalization(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                { 
                    spa.UseAngularCliServer(npmScript: "start");
                }
                
            });

            StaticServiceProvider.Initialize(app.ApplicationServices);
        }

        private void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAttributeService, AttributeService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProducerService, ProducerService>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddTransient<ISalesOrderService, SalesOrderService>();
            services.AddTransient<IPharmacyService, PharmacyService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IActiveSubstanceService, ActiveSubstanceService>();
            services.AddTransient<IBusinessIntelligenceService, BusinessIntelligenceService>();
        }

        private void ConfigureMvcService(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "ServerApp/Resources");
            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        private void ConfigureAutoMapperService(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }

        private void ConfigureDbContextService(IServiceCollection services)
        {
            services.AddDbContext<EPharmacyContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        private void LoadOptions(IServiceCollection services)
        {
            services.Configure<SendGridOptions>(Configuration.GetSection("SendGridOptions"));
            services.Configure<AzureStorageOptions>(Configuration.GetSection("AzureStorageOptions"));
        }

        private void ConfigureAzureServices(IServiceCollection services)
        {
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

            services.AddScoped<IMailSenderService, MailSenderService>();
        }

        private void ConfigureSpaService(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        private void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerDocument(settings =>
            {
                settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                settings.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = SwaggerSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}. You can get a JWT token from /api/Account/Login."
                }));

                settings.PostProcess = document =>
                {
                    document.Info.Version = "v0.1.0";
                    document.Info.Title = "EPharmacy API";
                    document.Info.Description = "EPharmacy API endpoints specification";
                };
            });
        }

        private void ConfigureAuthenticationService(IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, ApplicationRole>(identity =>
            {
                identity.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<EPharmacyContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer((cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:key"])),
                        ValidIssuer = Configuration["Token:issuer"],
                        ValidateIssuer = true,
                        ValidAudience = Configuration["Token:audience"],
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                }));
        }

        private void ConfigureRequestLocalization(IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("pl"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }
    }
}
