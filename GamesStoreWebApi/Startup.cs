using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GamesStoreWebApi.Helpers;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Repositories;
using GamesStoreWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GamesStoreWebApi
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
            services.AddDbContext<GameStoreContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("GSConnection"));
            });

            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => 
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        // Validate the JWT Issuer (iss) claim
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetSection("Jwt:Issuer").Value,
                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = true,
                        ValidAudience = Configuration.GetSection("Jwt:Audience").Value,
                        // Validate the token expiry
                        ValidateLifetime = true,
                        // If you want to allow a certain amount of clock drift, set that here:
                        ClockSkew = TimeSpan.Zero
                    };
                });

            //services.AddScoped<IUser, UserInfo>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<OrderDetailsRepository>();
            services.AddScoped<ReviewRepository>();

            services.AddCors(options => options.AddDefaultPolicy(builder => {

                // Fluent API
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("X-Pagination");
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
