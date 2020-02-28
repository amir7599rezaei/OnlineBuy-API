using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OnlineBuy.Data.DataContext;
using OnlineBuy.Presentation.CustomFilters;
using OnlineBuy.Repository.Infrastructure.Implements;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using OnlineBuy.Service.JWT.Implement;
using OnlineBuy.Service.JWT.Interface;
using Serilog;

namespace OnlineBuy.Presentation
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

            //services.AddMvc(options =>
            //{
            //    // All endpoints need authorization using our custom authorization filter
            //    //options.Filters.Add(new CustomAuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            //    //options.Filters.Add(new UnauthorizedFilter());
            //});

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<OnlineBuyContext>(option =>
                option.UseSqlServer(Configuration.GetSection("Appsettings:ConnectionString").Value));

            OnlineBuyContext.ConnectionString = Configuration.GetSection("Appsettings:ConnectionString").Value;

            services.AddCors();
            services.AddControllers();
            services.AddScoped<IUnitOfWork<OnlineBuyContext>, UnitOfWork<OnlineBuyContext>>();
            services.AddScoped<IJsonWebTokensService, JsonWebTokensService>();

            //add to use jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(opt =>
              {
                  opt.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Appsettings:Token").Value)),
                      ValidateIssuer = false,//duto use local
                      ValidateAudience = false//duto use local
                  };
              });
            
            //OnlineBuyContext.ConnectionString = Configuration.GetSection("Appsettings:ConnectionString").Value;
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                OnlineBuyContext.ConnectionString = Configuration.GetSection("Appsettings:ConnectionString").Value;
                var context = serviceScope.ServiceProvider.GetRequiredService<OnlineBuyContext>();
                context.Database.Migrate();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {                        
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });



            }

            //use to call methods for cross sites
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSerilogRequestLogging(); // <-- Add for serilog use

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
