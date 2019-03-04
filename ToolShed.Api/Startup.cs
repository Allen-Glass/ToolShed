using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ToolShed.IotHub.Interfaces;
using ToolShed.IotHub.Services;

namespace ToolShed
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
            services.AddCors(
                    options => options.AddPolicy("Dispenser",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    })
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
            });

            services.AddTransient<IIotActionServices, IotActionServices>(sp =>
            {
                var serviceClient = ServiceClient.CreateFromConnectionString("");
                return new IotActionServices(serviceClient);
            });

            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseCors("SignalR");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
