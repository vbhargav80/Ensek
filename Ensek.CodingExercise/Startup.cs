using System.Reflection;
using Ensek.CodingExercise.Domain.Commands;
using Ensek.CodingExercise.Domain.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ensek.CodingExercise.Domain.Services;
using Ensek.CodingExercise.Infrastructure;
using Ensek.CodingExercise.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ensek.CodingExercise
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ensek.CodingExercise", Version = "v1" });
            });

            services.AddMediatR(typeof(UploadMeterReadingCommand).GetTypeInfo().Assembly);

            services.AddScoped<IFileParsingService, FileParsingService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));

            services.AddDbContext<EnsekDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ensek.CodingExercise v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EnsekDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
