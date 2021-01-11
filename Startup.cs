using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MS2_DoTrongDuc.BL;
using MS2_DoTrongDuc.BL.Interfaces;
using MS2_DoTrongDuc.DL;
using MS2_DoTrongDuc.DL.Interfaces;

namespace WDT2020_MS2_DoTrongDuc_CukCuk
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
            //DbConnector.connectionString = Configuration.GetConnectionString("DucConnectionString");

            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoTrongDuc.API", Version = "v1" });
                c.CustomSchemaIds(type => type.ToString());
            });

            services.AddScoped<IEmployeeDL, EmployeeDL>();
            services.AddScoped<IEmployeeBL, EmployeeBL>();
            services.AddScoped<IDepartmentBL, DepartmentBL>();
            services.AddScoped<IDepartmentDL, DepartmentDL>();
            services.AddScoped<IJobBL, JobBL>();
            services.AddScoped<IJobDL, JobDL>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Duc.API Service V1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
