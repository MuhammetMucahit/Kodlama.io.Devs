using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext:DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<ProgrammingLanguageTechnology> ProgrammingLanguageTechnologies { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(x =>
            {
                x.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                x.Property(prop => prop.Id).HasColumnName("Id");
                x.Property(prop => prop.Name).HasColumnName("Name");
                x.HasMany(prop => prop.ProgrammingLanguageTechnologies);
            });

            modelBuilder.Entity<ProgrammingLanguageTechnology>(x =>
            {
                x.ToTable("ProgrammingLanguageTechnologies").HasKey(k => k.Id);
                x.Property(prop => prop.Id).HasColumnName("Id");
                x.Property(prop => prop.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                x.Property(prop => prop.Name).HasColumnName("Name");
                x.HasOne(prop => prop.ProgrammingLanguage);
            });

            ProgrammingLanguage[] programmingLanguages = { new() { Id = 1, Name = "C#" } };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguages);


            ProgrammingLanguageTechnology[] technologies =
            {
                new ProgrammingLanguageTechnology(1,"Spring",5)
            };
            modelBuilder.Entity<ProgrammingLanguageTechnology>().HasData(technologies);
        }
    }
}
