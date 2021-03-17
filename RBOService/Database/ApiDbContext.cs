using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RBOService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RBOService.Database
{
    public class ApiDbContext : DbContext
    {
        public SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ParameterEntity>(cfg =>
            {
                cfg.HasKey(p => p.Id);
                cfg.HasAlternateKey(p => p.ExternalId);
                cfg.HasIndex(p => p.Index)
                    .IsUnique();

                cfg.Property(p => p.Value)
                    .IsRequired()
                    .HasMaxLength(50);

                cfg.HasOne(p => p.Rule)
                    .WithMany(r => r.Parameters)
                    .IsRequired();
         
            });

            builder.Entity<RuleEntity>(cfg =>
            {
                cfg.HasKey(r => r.Id);
                cfg.HasAlternateKey(r => r.ExternalId);
                
                cfg.HasIndex(r => r.Index)
                    .IsUnique();
                
                cfg.Property(r => r.Pattern)
                    .IsRequired();
                
                cfg.Property(r => r.SourceType)
                    .IsRequired()
                    .HasConversion<string>(new EnumToStringConverter<SourceTypeEnum>());
                
                cfg.Property(r => r.DestinationType)
                    .IsRequired()
                    .HasConversion<string>(new EnumToStringConverter<DestinationTypeEnum>());

                cfg.HasMany(r => r.Parameters)
                    .WithOne(p => p.Rule)
                    .IsRequired();                

                cfg.HasOne(r => r.Group)
                    .WithMany(g => g.Rules)
                    .IsRequired();
            });

            builder.Entity<GroupEntity>(cfg =>
            {
                cfg.HasKey(g => g.Id);
                cfg.HasAlternateKey(g => g.ExternalId);

                cfg.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                cfg.HasMany(g => g.Rules)
                    .WithOne(r => r.Group)
                    .IsRequired();
            });

            builder.Entity<UserEntity>(cfg =>
            {
                cfg.HasKey(u => u.Id);

                cfg.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                cfg.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Entity<UserEntity>().HasData(new { Id=1, Username = "user", Password = "pass" });
                
        }
    }
}
