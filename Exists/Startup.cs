using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exists
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
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IsDevelopment()
                .UseCors(MakeBuilder)
                .UseHttpsRedirection()
                .UseMvc();

            IApplicationBuilder IsDevelopment() 
                => env.IsDevelopment() ? app.UseDeveloperExceptionPage() : app.UseHsts();

            void MakeBuilder(CorsPolicyBuilder builder)
                => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    }
}