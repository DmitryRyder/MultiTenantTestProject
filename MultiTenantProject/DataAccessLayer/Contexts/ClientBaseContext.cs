using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Contexts;

public class ClientBaseContext : DbContext
{
    public ClientBaseContext(DbContextOptions<ClientBaseContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedTestData(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private void SeedTestData(ModelBuilder modelBuilder)
    {
        var tenantId = Guid.NewGuid();

        var companies = Enumerable.Range(1, 5).Select(i => new Company
        {
            Id = Guid.NewGuid(),
            Name = $"Company {i}",
            Address = $"Address {i}",
            TenantId = tenantId
        }).ToList();

        var clients = Enumerable.Range(1, 3).Select(i =>
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = $"Client {i}",
                CompanyId = companies[i % companies.Count].Id,
                TenantId = tenantId
            };

            return client;

        }).ToList();

        modelBuilder.Entity<Company>().HasData(companies);
        modelBuilder.Entity<Client>().HasData(clients);
    }
}