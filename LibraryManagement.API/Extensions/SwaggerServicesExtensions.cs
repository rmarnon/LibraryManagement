﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LibraryManagement.API.Extensions
{
    public static class SwaggerServicesExtensions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new() { Title = "Library Management", Version = "v1" });

                s.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using Bearer scheme."
                });

                s.AddSecurityRequirement(new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configuration["AuthSettings:Issuer"],
                        ValidAudience = configuration["AuthSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]))
                    };

                });

            return services;
        }
    }
}