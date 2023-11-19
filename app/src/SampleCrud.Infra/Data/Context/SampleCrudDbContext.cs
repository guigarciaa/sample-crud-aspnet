using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SampleCrud.Domain.Entities;

namespace SampleCrud.Infra.Data.Context
{
    /// <summary>
    /// Represents the database context for the SampleCrud application.
    /// </summary>
    public class SampleCrudDbContext : DbContext
    {
        public SampleCrudDbContext() { }

        public SampleCrudDbContext(DbContextOptions<SampleCrudDbContext> options)
            : base(options) { }

        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<User>()
            .HasOne(e => e.PersonId)
            .WithOne(e => e.User)
            .HasForeignKey<Person>(e => e.Id)
            .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}