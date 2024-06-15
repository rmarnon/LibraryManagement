using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagement.API.Extensions;
using LibraryManagement.API.Filters;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Validators;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

const string Context = "LibraryDb";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiIoC();
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ValidationFilter)));

var connectionString = builder.Configuration.GetConnectionString(Context);
builder.Services.AddDbContext<LibraryDbContext>(s => s.UseSqlServer(connectionString));

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(CreateBookCommandHandler)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<LibraryDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management - API");
        s.DocExpansion(DocExpansion.List);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
