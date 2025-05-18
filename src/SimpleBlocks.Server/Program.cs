using Microsoft.EntityFrameworkCore;
using SimpleBlocks.Server.Api.DependencyInjection;
using SimpleBlocks.Server.Application.DependencyInjection;
using SimpleBlocks.Server.Infrastructure.DependencyInjection;
using SimpleBlocks.Server.Persistence;
using SimpleBlocks.Server.Persistence.DependencyInjection;
using SimpleBlocks.Server.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

var installers = new IServiceInstaller[]
{
    new ApiInstaller(),
    new ApplicationInstaller(),
    new PersistenceInstaller(),
    new InfrastructureInstaller()
};

foreach (var installer in installers)
{
    installer.InstallServices(builder.Services, builder.Configuration);
}

var allowedOrigins = builder.Configuration["CORS_ORIGINS"]?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

await DbSeeder.SeedAllAsync(app.Services);

app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();