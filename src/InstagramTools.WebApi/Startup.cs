using System;
using System.IO;
using System.Text;
using AutoMapper;
using InstagramTools.Api.API.Builder;
using InstagramTools.Common.Helpers;
using InstagramTools.Core.Implemenations;
using InstagramTools.Core.Interfaces;
using InstagramTools.Data;
using InstagramTools.WebApi.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace InstagramTools.WebApi
{
    public class Startup
    {
        //TODO: don't forget about this!
        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Settings/appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"Settings/appsettings.{env.EnvironmentName}.json", optional: false);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure DataBaseContext
            string connection = GetConnectionStringForMachine(Configuration);
            services.AddDbContext<InstagramToolsContext>(options => options.UseSqlServer(connection));

            //Configure AuthorizationOptions
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.UserMinimum,
                                      policy => policy.RequireClaim(Constants.ClaimTypes.RoleName,
                                                    Constants.Roles.User, Constants.Roles.Moderator,
                                                    Constants.Roles.Admin));

                options.AddPolicy(Constants.Policies.ModeratorMinimum,
                                  policy => policy.RequireClaim(Constants.ClaimTypes.RoleName,
                                                Constants.Roles.Moderator, Constants.Roles.Admin));

                options.AddPolicy(Constants.Policies.AdminOnly,
                                  policy => policy.RequireClaim(Constants.ClaimTypes.RoleName,
                                                Constants.Roles.Admin));
            });

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSingleton(Configuration);
            ConnectServices(services);
            services.AddMvc()
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //Add AutoMapper. We also can configure to scan mappings by assebbly name to resove dependencies: https://github.com/AutoMapper/AutoMapper/wiki/Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile(Configuration));
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Setup Lowercase Urls
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //Get JWT options
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            //Config token validator
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            //Add JsonWebToken middleware
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            //app.UseApplicationInsightsRequestTelemetry();

            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();
        }

        #region Tools

        private void ConnectServices(IServiceCollection services)
        {
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            
            
            services.AddScoped<IInstaToolsService, InstaToolsService>();
            services.AddScoped<IInstaApiBuilder, InstaApiBuilder>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminRoleService, AdminRoleService>();
            services.AddScoped<IRoleService, RoleService>();
        }

        //Return DbConnString based on Machine's name
        private string GetConnectionStringForMachine(IConfigurationRoot configuration)
        {
            if (Environment.MachineName.Equals("SERGIEVSKIY-DEV", StringComparison.OrdinalIgnoreCase))
            {
                return configuration.GetConnectionString("AzureTestDB");
            }
            if (Environment.MachineName.Equals("DESKTOP-PSG14TN", StringComparison.OrdinalIgnoreCase))
            {
                return configuration.GetConnectionString("AzureTestDB");
            }
            if (Environment.MachineName.Equals("SERGIEVSKIY-LENOVO", StringComparison.OrdinalIgnoreCase))
            {
                return configuration.GetConnectionString("lenovo-local");
            }
            return configuration.GetConnectionString("AzureTestDB");
        }

        #endregion
    }
}
