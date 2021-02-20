using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PizzaDelicious.Api.UI.Setup;
using PizzaDelicious.Catalog.Data;
using PizzaDelicious.Payments.Data;
using PizzaDelicious.Register.Data;
using PizzaDelicious.Sale.Data;

namespace PizzaDelicious.Api.UI
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
            services.AddDbContext<RegisterContext>(
                  options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly("PizzaDelicious.Register.Data"))
                 );

            services.AddDbContext<CatalogContext>(
           options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
           b => b.MigrationsAssembly("PizzaDelicious.Catalog.Data"))
          );


            services.AddDbContext<SaleContext>(
           options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
           b => b.MigrationsAssembly("PizzaDelicious.Sale.Data"))
          );

            services.AddDbContext<PaymentContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("PizzaDelicious.Payments.Data"))
            );



            services.AddAutoMapper(typeof(PizzaDelicious.Register.Application.AutoMapper.DomainToViewModelMappingProfile), typeof(PizzaDelicious.Register.Application.AutoMapper.ViewModelToDomainMappingProfile));
            services.AddAutoMapper(typeof(PizzaDelicious.Catalog.Application.AutoMapper.DomainToViewModelMappingProfile), typeof(PizzaDelicious.Catalog.Application.AutoMapper.ViewModelToDomainMappingProfile));

            services.AddControllers()
                .AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                        );

            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "PizzaDelicious",
                        Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()
                    }
                );
            });


            services.AddMediatR(typeof(Startup));
            services.RegisterServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"));
        }
    }
}
