using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Models;

namespace TestTask
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppointmentContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("MovieContext")));

            services.AddDbContext<CategoryContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("MovieContext")));

            services.AddCors();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:3293");
            }));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder =>
                builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
