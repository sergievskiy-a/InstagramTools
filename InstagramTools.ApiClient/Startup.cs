using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstagramTools.Api.API;
using InstagramTools.Api.API.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InstagramTools.Core.Implemenations;
using InstagramTools.Core.Interfaces;
using InstagramTools.Data;
using Microsoft.EntityFrameworkCore;

namespace InstagramTools.ApiClient
{
    public class Startup
    {
        //public Startup(IConfigurationRoot configuration)
        //{
        //    Configuration = configuration;
        //}


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
            this.Configuration = builder.Build();
        }




        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<IConfigurationRoot>(Configuration);


            string connection = this.GetConnectionStringForMachine(Configuration);
            services.AddDbContext<InstagramToolsContext>(options => options.UseSqlServer(connection));

            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();



            services.AddScoped<IInstaToolsService, InstaToolsService>();
            services.AddScoped<IInstaApiBuilder, InstaApiBuilder>();
            services.AddScoped<IInstaApi, InstaApi>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }

        private string GetConnectionStringForMachine(IConfigurationRoot configuration)
        {
            return configuration.GetConnectionString("RemoteConnection");
        }
    }
}
