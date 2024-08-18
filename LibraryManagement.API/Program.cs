using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagement.API.Extensions;
using LibraryManagement.API.Filters;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.Validators.Users;
using LibraryManagement.Infrastructure.Extensions;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Diagnostics.CodeAnalysis;

const string Context = "ConnectionStrings:LibraryDb";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthIoc();
builder.Services.AddRepositoryIoc();
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ValidationFilter)));

var connectionString = builder.Configuration.GetValue<string>(Context);
builder.Services.AddDbContext<LibraryDbContext>(s => s.UseSqlServer(connectionString));

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(
        connectionString,
        sinkOptions: new MSSqlServerSinkOptions()
        {
            AutoCreateSqlTable = true,
            TableName = "Logs",
        })
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

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
        var exist = context.Database.GetPendingMigrations().Any();

        if (exist)
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

[ExcludeFromCodeCoverage]
public partial class Program { }
