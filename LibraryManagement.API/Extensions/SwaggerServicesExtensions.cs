using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LibraryManagement.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerServicesExtensions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services, ConfigurationManager configuration)
        {
            const string Title = "Library - Management";
            const string Version = "1.0";
            const string Description = "Library management service";

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new() 
                { 
                    Title = Title, 
                    Version = Version, 
                    Description = Description,
                    Contact = new OpenApiContact
                    {
                        Name = "Rodrigo Marnon",
                        Email = "rmarnon@yahoo.com.br",
                        Url = new Uri("https://www.linkedin.com/in/rodrigo-marnon/")
                    }
                });

                var xmlFile = "LibraryManagement.API.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                s.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using Bearer scheme."
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement() 
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]!))
                    };
                });

            return services;
        }
    }
}
