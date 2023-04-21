using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppServer.Models;

public partial class SatisDbContext : DbContext
{
    //DbFirst esas alındı
    public SatisDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SatisDbContext(DbContextOptions<SatisDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
    readonly IConfiguration _configuration;
    public virtual DbSet<Personeller> Personellers { get; set; }

    public virtual DbSet<Satislar> Satislars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SQL"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Personeller>(entity =>
        {
            entity.ToTable("Personeller");
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Adi).HasMaxLength(50);
            entity.Property(e => e.Soyad).HasMaxLength(50);
        });

        modelBuilder.Entity<Satislar>(entity =>
        {
            entity.ToTable("Satislar");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
