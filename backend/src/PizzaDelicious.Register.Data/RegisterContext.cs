using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PizzaDelicious.Core.Data;
using PizzaDelicious.Core.Messages;
using PizzaDelicious.Register.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Register.Data
{
    public class RegisterContextFactory : IDesignTimeDbContextFactory<RegisterContext>
    {
        /*
         Pra rodar o migration
            - Add-Migration CreateTableClients -StartupProject PizzaDelicious.Register.Data -context RegisterContext -outputDir Migrations
            - Update-Database -StartupProject PizzaDelicious.Register.Data
         
         */
        public RegisterContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RegisterContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new RegisterContext(builder.Options);
        }
    }

    public class RegisterContext : DbContext, IUnitOfWork
    {
        public RegisterContext(DbContextOptions<RegisterContext> options)
            : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) ;
            
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegisterContext).Assembly);
        }


        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisterDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegisterDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegisterDate").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
