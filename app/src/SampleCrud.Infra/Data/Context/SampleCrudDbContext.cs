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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            base.OnModelCreating(modelBuilder);
        }
    }
}