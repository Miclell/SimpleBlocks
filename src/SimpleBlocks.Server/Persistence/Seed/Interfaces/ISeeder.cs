namespace SimpleBlocks.Server.Persistence.Seed.Interfaces;

public interface ISeeder
{
    Task SeedAsync(AppDbContext context);
}