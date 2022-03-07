using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebshopAPI.Core.ApplicationService;
using WebshopAPI.Core.ApplicationService.Services;
using WebshopAPI.Core.DomainService;
using WebshopAPI.Infrastructure.Data.Repositories;

namespace WebshopRestAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Product
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            // Basket
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            // ProductStatus
            services.AddScoped<IProductStatusService, ProductStatusService>();
            services.AddScoped<IProductStatusRepository, ProductStatusRepository>();

            // User
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            var authOptions = Configuration.GetSection("FirebaseAuth").Get<FirebaseAuthOptions>();
            services.AddScoped<IFirebaseAuthService>(u => new FirebaseAuthService(authOptions));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowSpecificOrigin");
            }
            else
            {
                app.UseCors("AllowSpecificOrigin");
                // app.UseHsts();
            }


            app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
