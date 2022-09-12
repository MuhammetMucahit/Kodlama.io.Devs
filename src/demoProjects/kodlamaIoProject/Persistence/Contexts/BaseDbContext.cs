using Core.Security.Entities;
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

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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

            modelBuilder.Entity<User>(p =>
            {
                p.ToTable("Users").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.FirstName).HasColumnName("FirstName");
                p.Property(p => p.LastName).HasColumnName("LastName");
                p.Property(p => p.Email).HasColumnName("Email");
                p.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                p.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                p.Property(p => p.Status).HasColumnName("Status").HasDefaultValue(true);
                p.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
                p.HasMany(p => p.UserOperationClaims);
                p.HasMany(p => p.RefreshTokens);
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.HasOne(p => p.OperationClaim);
                a.HasOne(p => p.User);
            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });
            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.ToTable("RefreshTokens").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
                a.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
                a.Property(p => p.Created).HasColumnName("Created");
                a.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
                a.Property(p => p.Expires).HasColumnName("Expires");
                a.Property(p => p.ReasonRevoked).HasColumnName("UseReasonRevokedrId");
                a.Property(p => p.Revoked).HasColumnName("Revoked");
                a.Property(p => p.Token).HasColumnName("Token");
                a.HasOne(p => p.User);

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
