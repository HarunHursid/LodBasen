﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LodBasen.Models;

public partial class lodbasen_dk_db_lodbasenContext : DbContext
{
    public lodbasen_dk_db_lodbasenContext()
    {
    }

    public lodbasen_dk_db_lodbasenContext(DbContextOptions<lodbasen_dk_db_lodbasenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Barn> Børn { get; set; }

    public virtual DbSet<Gruppe> Grupper { get; set; }

    public virtual DbSet<Leder> Ledere { get; set; }

    public virtual DbSet<Lodsalg> Lodsalgssamling { get; set; }

    public virtual DbSet<Lodseddel> Lodsedler { get; set; }

    public virtual DbSet<Modtager> Modtagere { get; set; }

    public virtual DbSet<Sælger> Sælgere { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=mssql5.unoeuro.com;Initial Catalog=lodbasen_dk_db_lodbasen;Persist Security Info=True;User ID=lodbasen_dk;Password=ne2GgdbHy3DEmrkAFwtx;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__4A300117ECBF5A00");
        });

        modelBuilder.Entity<Barn>(entity =>
        {
            entity.HasKey(e => e.BarnId).HasName("PK__Barn__6441CF44CAD3E651");

            entity.HasOne(d => d.Gruppe).WithMany(p => p.Børn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Barn__Gruppe_ID__25518C17");
        });

        modelBuilder.Entity<Gruppe>(entity =>
        {
            entity.HasKey(e => e.GruppeId).HasName("PK__Gruppe__E80B3140B8B51D2C");
        });

        modelBuilder.Entity<Leder>(entity =>
        {
            entity.HasKey(e => e.LederId).HasName("PK__Leder__72A5DB887A4B0345");

            entity.HasOne(d => d.Gruppe).WithMany(p => p.Ledere)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leder__Gruppe_ID__22751F6C");
        });

        modelBuilder.Entity<Lodsalg>(entity =>
        {
            entity.HasKey(e => e.LodsalgsId).HasName("PK__Lodsalg__E256EC5E3840697A");

            entity.HasOne(d => d.Lodseddel).WithMany(p => p.Lodsalgssamling)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lodsalg__Lodsedd__339FAB6E");

            entity.HasOne(d => d.Modtager).WithMany(p => p.Lodsalgssamling)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lodsalg__Modtage__32AB8735");

            entity.HasOne(d => d.Sælger).WithMany(p => p.Lodsalgssamling)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lodsalg__Sælger___31B762FC");
        });

        modelBuilder.Entity<Lodseddel>(entity =>
        {
            entity.HasKey(e => e.LodseddelId).HasName("PK__Lodsedde__502124E28CE0E3CE");
            entity.Property(e => e.LodseddelId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Modtager>(entity =>
        {
            entity.HasKey(e => e.ModtagerId).HasName("PK__Modtager__ECF0393A7D138BFA");
            entity.Property(e => e.ModtagerId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Barn).WithMany(p => p.Modtagere).HasConstraintName("FK__Modtager__Barn_I__2B0A656D");

            entity.HasOne(d => d.Leder).WithMany(p => p.Modtagere).HasConstraintName("FK__Modtager__Leder___2A164134");
        });

        modelBuilder.Entity<Sælger>(entity =>
        {
            entity.HasKey(e => e.SælgerId).HasName("PK__Sælger__0875BA34586244EA");
            entity.Property(e => e.SælgerId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Admin).WithMany(p => p.Sælgere).HasConstraintName("FK__Sælger__Admin_ID__2EDAF651");

            entity.HasOne(d => d.Leder).WithMany(p => p.Sælgere).HasConstraintName("FK__Sælger__Leder_ID__2DE6D218");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}