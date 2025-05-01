using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeder;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
// Add services to the container.
    builder.AddPresentation();
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);


    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantDataSeeder>();
    await seeder.SeedAsync();

    app.UseMiddleware<RequestTimeLoggingMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
    // if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    // {
        app.UseSwagger();
        app.UseSwaggerUI();
    // }

    app.UseHttpsRedirection();
    app.MapGroup("api/Identity")
        .WithTags("Identity").MapIdentityApi<User>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program
{
}