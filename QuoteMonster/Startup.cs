using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuoteMonster.Model;
using Microsoft.EntityFrameworkCore;
using QuoteMonster.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace QuoteMonster
{
	public class Startup
    {
		public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets<Startup>();
			}

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			// Add global cfg.
			services.AddSingleton<IConfigurationRoot>(Configuration);
			services.AddScoped<IUserManagementService, UserManagementService>();
			
			services.AddDbContext<PropertyContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("dbmain")));

			services.AddDbContext<QuoteContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("dbmain")));

			// Add framework services.
			services.AddMvc().AddJsonOptions(options => {
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});

			services.AddCors();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });				
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

			app.UseCors(cfg =>
				cfg.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod());

			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				// Had to add this in as per: http://stackoverflow.com/questions/37467620/asp-net-core-rc2-jwt-token-kid-error
				Authority = $"https://{Configuration["Auth0:Domain"]}/",
				Audience = Configuration["Auth0:ApiIdentifier"],
				RequireHttpsMetadata = false
			});

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "Home", action = "Index" });

				routes.MapSpaFallbackRoute(
					"spa-fallback", new
					{
						controller = "Home",
						action = "Index"
					});
			});
		}
    }
}
