﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Shared.Model;

public partial class WorksContext : DbContext
{
    public WorksContext()
    {
    }

    public WorksContext(DbContextOptions<WorksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminPlaceTb> AdminPlaceTbs { get; set; }

    public virtual DbSet<AdminTb> AdminTbs { get; set; }

    public virtual DbSet<BuildingTb> BuildingTbs { get; set; }

    public virtual DbSet<DepartmentTb> DepartmentTbs { get; set; }

    public virtual DbSet<EnergyMonthUsageTb> EnergyMonthUsageTbs { get; set; }

    public virtual DbSet<EnergyUsageTb> EnergyUsageTbs { get; set; }

    public virtual DbSet<FloorTb> FloorTbs { get; set; }

    public virtual DbSet<MaterialTb> MaterialTbs { get; set; }

    public virtual DbSet<MeterItemTb> MeterItemTbs { get; set; }

    public virtual DbSet<MeterReaderTb> MeterReaderTbs { get; set; }

    public virtual DbSet<PlaceTb> PlaceTbs { get; set; }

    public virtual DbSet<RoomTb> RoomTbs { get; set; }

    public virtual DbSet<StoreTb> StoreTbs { get; set; }

    public virtual DbSet<UnitTb> UnitTbs { get; set; }

    public virtual DbSet<UserTb> UserTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3307;database=works;user id=root;password=stecdev1234!", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.7-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<AdminPlaceTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.AdminTb).WithMany(p => p.AdminPlaceTbs).HasConstraintName("fk_ADMIN_TB_has_PLACE_ADMIN_TB1");

            entity.HasOne(d => d.Place).WithMany(p => p.AdminPlaceTbs).HasConstraintName("fk_ADMIN_TB_has_PLACE_PLACE1");
        });

        modelBuilder.Entity<AdminTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.DepartmentTb).WithMany(p => p.AdminTbs).HasConstraintName("fk_ADMIN_TB_DEPARTMENT_TB1");

            entity.HasOne(d => d.UserTb).WithMany(p => p.AdminTbs).HasConstraintName("fk_ADMIN_TB_USER_TB");
        });

        modelBuilder.Entity<BuildingTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.PlaceTb).WithMany(p => p.BuildingTbs).HasConstraintName("fk_BUILDING_TB_PLACE_TB1");
        });

        modelBuilder.Entity<DepartmentTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");
        });

        modelBuilder.Entity<EnergyMonthUsageTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasOne(d => d.MeterReaderTb).WithMany(p => p.EnergyMonthUsageTbs).HasConstraintName("fk_ENERGY_MONTH_USAGE_TB_METER_READER_TB1");
        });

        modelBuilder.Entity<EnergyUsageTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.MeterItemTb).WithMany(p => p.EnergyUsageTbs).HasConstraintName("fk_ENERGY_USAGE_TB_METER_ITEM_TB1");
        });

        modelBuilder.Entity<FloorTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.BuildingTb).WithMany(p => p.FloorTbs).HasConstraintName("fk_FLOOR_TB_BUILDING_TB1");
        });

        modelBuilder.Entity<MaterialTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasComment("자재 인덱스");
            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.Mfr).HasComment("제조사");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");
        });

        modelBuilder.Entity<MeterItemTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.MeterItem).HasComment("검침항목");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.MeterReaderTb).WithMany(p => p.MeterItemTbs).HasConstraintName("fk_METER_ITEM_TB_METER_READER_TB1");
        });

        modelBuilder.Entity<MeterReaderTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.Type).HasComment("계약종별");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.BuildingTb).WithMany(p => p.MeterReaderTbs).HasConstraintName("fk_METER_READER_TB_BUILDING_TB1");
        });

        modelBuilder.Entity<PlaceTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");
        });

        modelBuilder.Entity<RoomTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.FloorTb).WithMany(p => p.RoomTbs).HasConstraintName("fk_ROOM_TB_FLOOR_TB1");
        });

        modelBuilder.Entity<StoreTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.Price).HasComment("금액\n");
            entity.Property(e => e.UnitPrice).HasComment("단가\n");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.BuildingTb).WithMany(p => p.StoreTbs).HasConstraintName("fk_STORE_TB_BUILDING_TB1");

            entity.HasOne(d => d.MaterialTb).WithMany(p => p.StoreTbs).HasConstraintName("fk_STORE_TB_MATERIAL_TB1");

            entity.HasOne(d => d.RoomTb).WithMany(p => p.StoreTbs).HasConstraintName("fk_STORE_TB_ROOM_TB1");
        });

        modelBuilder.Entity<UnitTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.PlaceTb).WithMany(p => p.UnitTbs).HasConstraintName("fk_UNIT_PLACE_TB1");
        });

        modelBuilder.Entity<UserTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.AdminYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.AlramYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.CreateDt).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.DelYn).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermBuilding).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermClaim).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermComp).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermConst).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermEmployee).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermEnergy).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermEquipment).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermLawCk).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermLawEdu).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermMaterial).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermOffice).HasDefaultValueSql("'0'");
            entity.Property(e => e.PermSys).HasDefaultValueSql("'0'");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdateDt).HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.PlaceTb).WithMany(p => p.UserTbs).HasConstraintName("fk_USER_TB_PLACE_TB1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}