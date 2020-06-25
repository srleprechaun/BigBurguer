using BigBurguer.Api.Infrastructure.Models;
using BigBurguer.Api.Services;
using BigBurguer.Api.Views;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;

namespace BigBurguer.Api
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjection(services);

            services
                .AddDbContextPool<AppDbContext>(
                options => options
                    .UseSqlServer(_config
                    .GetConnectionString("BigBurguerDBConnection")));

            services.AddCors(options =>
            {
                options
                .AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services
                .AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_config.GetValue<string>("JWTSecretKey"))
                    )
                };
            });

            services.AddSingleton<IAuthService>(
                new AuthService(
                    _config.GetValue<string>("JWTSecretKey"),
                    _config.GetValue<int>("JWTLifespan")
                )
            );

            services
               .AddMvc()
               .AddFluentValidation()
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
               .AddControllersAsServices();
        }

        private static void DependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddTransient<IValidator<IngredientViewModel>, IngredientValidator>();
            services.AddTransient<IValidator<ProductViewModel>, ProductValidator>();
            services.AddTransient<IValidator<CustomerViewModel>, CustomerValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "BigBurguerAPI");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
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
