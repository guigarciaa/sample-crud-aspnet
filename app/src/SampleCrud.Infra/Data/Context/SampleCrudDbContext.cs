using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SampleCrud.Domain.Entities;

namespace SampleCrud.Infra.Data.Context
{
    public class SampleCrudDbContext : DbContext
    {
        public SampleCrudDbContext() { }

        public SampleCrudDbContext(DbContextOptions<SampleCrudDbContext> options)
            : base(options) { }

        public virtual DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<User>()
            .HasOne(e => e.PersonId)
            .WithOne(e => e.UserId)
            .HasForeignKey<Person>(e => e.Id)
            .IsRequired();

            modelBuilder.Entity<Person>()
            .HasOne(e => e.UserId)
            .WithOne(e => e.PersonId)
            .HasForeignKey<User>(e => e.Id)
            .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}