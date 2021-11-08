
using Application.Interface.Security;
using Application.Service.Security;

using DistribuitedServices.Core.Middlewares;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Security.JWT;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Infrastructure.Transversal.Core.Accessor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Domain.Security.Repositories;
using Infrastructure.Security;
using Application.Adapter.Core;
using AutoMapper;
using Application.DTO.Security.Profiles;
using Infrastructure.Security.Authorization;
using Application.Service.Security.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace DistribuitedServices.Security.RestApi
{
    public class Startup
    {
        private readonly string myAllowAllOrigins = "_myAllowAllOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMiddleware<CustomExceptionMiddleware>();
//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//#if DEBUG
//                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//#else
//                c.SwaggerEndpoint("/Security/swagger/v1/swagger.json", "My API V1");
//#endif

//            });

            app.UseCors(myAllowAllOrigins);
            app.UseMvc();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            option.AddPolicy(myAllowAllOrigins,
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            }));

            services.AddMemoryCache();

            services.AddSingleton<IAdapter>(x => new AdapterAutoMapper(new Profile[] {
                new UsersProfile()
            }));

            //JWTFactory
            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddScoped<IUsersAppService, UsersAppService>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            var jwtOptions = Configuration.GetSection(nameof(JwtOptions));

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["APIKeyJWT"])),
                        SecurityAlgorithms.HmacSha256
                        );
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions[nameof(JwtOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtOptions[nameof(JwtOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["APIKeyJWT"])),

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtOptions[nameof(JwtOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IContextAccessor, ContextAccessor>();

            services.AddLogging(lg =>
            {
                lg.AddConfiguration(Configuration.GetSection("Logging"));
                lg.AddConsole();
                lg.AddDebug();
            });

            //Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

            //    //Locate the XML file being generated by ASP.NET.../

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    //var xmlPath = Path.Combine("c:\\XMLTeste", xmlFile);
            //    //... and tell Swagger to use those XML comments.
            //    c.IncludeXmlComments(xmlPath);
            //});

            services.AddMvc(x => x.EnableEndpointRouting = false);

        }
    }
}
