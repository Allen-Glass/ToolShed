using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ToolShed.IotHub.Interfaces;
using ToolShed.IotHub.Services;
using ToolShed.Renting;
using ToolShed.Repository.Context;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;
using ToolShed.Repository.Services;

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
            var sqlConnection = Configuration["SQLConnectionString"];
            var iotHubConnectionString = Configuration["IotHubConnectionString"];

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

            services.AddSingleton(sp => { return new RandomCodeGenerator(6); });
            services.AddTransient<DispenserRepository>();
            services.AddTransient<CardRepository>();
            services.AddTransient<AddressRepository>();
            services.AddTransient<DispenserToolsRepository>();
            services.AddTransient<UserAddressesRepository>();
            services.AddTransient<UserCardRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<ICardSQLService, CardSQLService>();
            services.AddTransient<IDispenserSQLService, DispenserSQLService>();
            services.AddTransient<IIotActionServices, IotActionServices>(sp =>
            {
                var serviceClient = ServiceClient.CreateFromConnectionString(iotHubConnectionString);
                return new IotActionServices(serviceClient);
            });

            services.AddSignalR();
            services.AddDbContext<ToolShedContext>(options => options.UseSqlServer(sqlConnection), ServiceLifetime.Transient);
            services.AddDbContext<TenantContext>(options => options.UseSqlServer(sqlConnection), ServiceLifetime.Transient);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication()
                .AddMicrosoftAccount()
                .AddGoogle()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["FacebookAppId"];
                });
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
            app.UseCors("Dispenser");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
