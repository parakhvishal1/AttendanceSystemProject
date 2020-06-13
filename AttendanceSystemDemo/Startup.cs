using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AttendanceSystemDemo.Models;
using AttendanceSystemDemo.Contracts;
using AttendanceSystemDemo.Repositories;
using AttendanceSystemDemo.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

namespace AttendanceSystemDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddMvc();
           /* var connection = "Data Source=AttendanceSystemDb,1433;Initial Catalog=AttendanceSystemDemo;Persist Security Info=False;User ID=YOUR_USER_ID_HERE;Password=YOUR_PASSWORD_HERE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            services.AddDbContext<AttendanceContext>(options => options.UseSqlite(connection));
            services.AddDbContext<AttendanceContext>(options => options.UseSqlite("Data Source=AttendanceSystemDb"));
            services.AddControllersWithViews();
            services.AddDbContext<AttendanceContext>(options =>
            options.UseSqlite("Data Source=AttendanceSystemDb"));*/
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureSqliteContext(Configuration);


            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
