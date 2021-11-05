using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Tooling;

namespace AksStartupDotnetApp
{
    public class Startup
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly string swaggerVer = "3.3";

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerVer,
                    new OpenApiInfo { Title = "Dotnet app for AKS-STARTUP", Version = swaggerVer });
                c.DescribeAllParametersInCamelCase();
            });

            // var connectionString = GetSqlConnectionString();
            // //var connectionString = Configuration.GetConnectionString("sqlConnection");            
            // services.AddDbContext<AksStartupDotnetAppContext>(options => options.UseSqlServer(connectionString));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // specifying the Swagger JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerVer}/swagger.json", "Api - AksStartupDotnetApp");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string GetSqlConnectionString()
        {
            var envValue = EnvironmentService.GetSqlConnectionString();
            if (!string.IsNullOrEmpty(envValue))
                return envValue;
            return Configuration.GetConnectionString("sqlConnection");

        }
    }
}
