using System;
using System.Collections.Generic;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace FamTec.Server.Databases;

public partial class FmsContext : DbContext
{
    public FmsContext()
    {
    }

    public FmsContext(DbContextOptions<FmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlarmsTb> AlarmsTbs { get; set; }

    public virtual DbSet<BuildingsTb> BuildingsTbs { get; set; }

    public virtual DbSet<EnergyUsagesTb> EnergyUsagesTbs { get; set; }

    public virtual DbSet<FacilitysTb> FacilitysTbs { get; set; }

    public virtual DbSet<FloorsTb> FloorsTbs { get; set; }

    public virtual DbSet<KakaoLogsTb> KakaoLogsTbs { get; set; }

    public virtual DbSet<MaterialsTb> MaterialsTbs { get; set; }

    public virtual DbSet<MeterReadersTb> MeterReadersTbs { get; set; }

    public virtual DbSet<PlacesTb> PlacesTbs { get; set; }

    public virtual DbSet<RoomInventorysTb> RoomInventorysTbs { get; set; }

    public virtual DbSet<RoomsTb> RoomsTbs { get; set; }

    public virtual DbSet<SocketRoomsTb> SocketRoomsTbs { get; set; }

    public virtual DbSet<SubItemsTb> SubItemsTbs { get; set; }

    public virtual DbSet<TotalInventorysTb> TotalInventorysTbs { get; set; }

    public virtual DbSet<UnitPriceTb> UnitPriceTbs { get; set; }

    public virtual DbSet<UsersTb> UsersTbs { get; set; }

    public virtual DbSet<VocCommentsTb> VocCommentsTbs { get; set; }

    public virtual DbSet<VocTb> VocTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=123.2.156.122,1002;Database=FMS;User Id=stec;Password=stecdev1234!;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlarmsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ALARMS_T__3214EC271D8D3E5D");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.SocketRooms).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__SOCKE__23893F36");

            entity.HasOne(d => d.UsersUser).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__USERS__22951AFD");

            entity.HasOne(d => d.Voc).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__VOC_I__21A0F6C4");
        });

        modelBuilder.Entity<BuildingsTb>(entity =>
        {
            entity.HasKey(e => e.BuildingCd).HasName("PK__BUILDING__1E409687EBA112AD");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.BuildingsTbs).HasConstraintName("FK__BUILDINGS__PLACE__01342732");
        });

        modelBuilder.Entity<EnergyUsagesTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ENERGY_U__3214EC27449D7BE3");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<FacilitysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FACILITY__3214EC276D4CF6DD");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.FacCreateDt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Rooms).WithMany(p => p.FacilitysTbs).HasConstraintName("FK__FACILITYS__ROOMS__2942188C");
        });

        modelBuilder.Entity<FloorsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FLOORS_T__3214EC27ACDCD733");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.FloorsTbs).HasConstraintName("FK__FLOORS_TB__BUILD__05F8DC4F");
        });

        modelBuilder.Entity<KakaoLogsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KAKAO_LO__3214EC272E2E48B5");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MaterialsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MATERIAL__3214EC277B42B210");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MeterReadersTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__METER_RE__3214EC27FE0EE104");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.MeterReadersTbs).HasConstraintName("FK__METER_REA__BUILD__49AEE81E");
        });

        modelBuilder.Entity<PlacesTb>(entity =>
        {
            entity.HasKey(e => e.PlaceCd).HasName("PK__PLACES_T__1E23CE2BD3E526C4");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<RoomInventorysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOM_INV__3214EC27C9DFD983");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.RoomInventorysTbs).HasConstraintName("FK__ROOM_INVE__BUILD__4119A21D");

            entity.HasOne(d => d.Rooms).WithMany(p => p.RoomInventorysTbs).HasConstraintName("FK__ROOM_INVE__ROOMS__40257DE4");
        });

        modelBuilder.Entity<RoomsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOMS_TB__3214EC27B00B7AE8");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Floor).WithMany(p => p.RoomsTbs).HasConstraintName("FK__ROOMS_TB__FLOOR___0ABD916C");
        });

        modelBuilder.Entity<SocketRoomsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SOCKET_R__3214EC27FD4FECE6");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<SubItemsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SUB_ITEM__3214EC27D0F45070");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.SubItemsTbs).HasConstraintName("FK__SUB_ITEMS__BUILD__39788055");

            entity.HasOne(d => d.Facility).WithMany(p => p.SubItemsTbs).HasConstraintName("FK__SUB_ITEMS__FACIL__3A6CA48E");
        });

        modelBuilder.Entity<TotalInventorysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TOTAL_IN__3214EC27F19A4322");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.TotalInventorysTbs).HasConstraintName("FK__TOTAL_INV__BUILD__34B3CB38");

            entity.HasOne(d => d.Material).WithMany(p => p.TotalInventorysTbs).HasConstraintName("FK__TOTAL_INV__MATER__33BFA6FF");
        });

        modelBuilder.Entity<UnitPriceTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UNIT_PRI__3214EC27194E859D");

            entity.HasOne(d => d.EnveryUsage).WithMany(p => p.UnitPriceTbs).HasConstraintName("FK__UNIT_PRIC__ENVER__515009E6");

            entity.HasOne(d => d.MeterReader).WithMany(p => p.UnitPriceTbs).HasConstraintName("FK__UNIT_PRIC__METER__505BE5AD");
        });

        modelBuilder.Entity<UsersTb>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERS_TB__F3BEEBFF564D2827");

            entity.Property(e => e.AdminYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.AlarmYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.UsersTbs).HasConstraintName("FK__USERS_TB__PLACEC__7C6F7215");
        });

        modelBuilder.Entity<VocCommentsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VOC_COMM__3214EC27E4864003");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.UsersUser).WithMany(p => p.VocCommentsTbs).HasConstraintName("FK__VOC_COMME__USERS__18178C8A");

            entity.HasOne(d => d.Voc).WithMany(p => p.VocCommentsTbs).HasConstraintName("FK__VOC_COMME__VOC_I__17236851");
        });

        modelBuilder.Entity<VocTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VOC_TB__3214EC27FB8E45C7");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.ReplyYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.VocTbs).HasConstraintName("FK__VOC_TB__BUILDING__116A8EFB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
