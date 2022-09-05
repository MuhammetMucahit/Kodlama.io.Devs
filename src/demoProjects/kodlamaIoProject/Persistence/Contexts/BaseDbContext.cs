using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext:DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(x =>
            {
                x.ToTable("ProgrammingLanguages");
                x.Property(prop => prop.Id).HasColumnName("Id");
                x.Property(prop => prop.Name).HasColumnName("Name");
            });

            //ProgrammingLanguage[] programmingLanguages = { new() { Id = 1, Name = "C#" } };
            //modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguages);
        }
    }
}
