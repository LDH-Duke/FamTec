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

    public virtual DbSet<AdminPlacesTb> AdminPlacesTbs { get; set; }

    public virtual DbSet<AdminsTb> AdminsTbs { get; set; }

    public virtual DbSet<AlarmsTb> AlarmsTbs { get; set; }

    public virtual DbSet<BuildingsTb> BuildingsTbs { get; set; }

    public virtual DbSet<EnergyMonthUsageTb> EnergyMonthUsageTbs { get; set; }

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

    public virtual DbSet<UnitTb> UnitTbs { get; set; }

    public virtual DbSet<UsersTb> UsersTbs { get; set; }

    public virtual DbSet<VocCommentsTb> VocCommentsTbs { get; set; }

    public virtual DbSet<VocTb> VocTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=123.2.156.122,1002;Database=FMS;User Id=stec;Password=stecdev1234!;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminPlacesTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ADMIN_PL__3214EC276B282138");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.AdminPlacesTbs).HasConstraintName("FK__ADMIN_PLA__PLACE__2374309D");

            entity.HasOne(d => d.User).WithMany(p => p.AdminPlacesTbs).HasConstraintName("FK__ADMIN_PLA__USER___22800C64");
        });

        modelBuilder.Entity<AdminsTb>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__ADMINS_T__F3BEEBFFFFD428DE");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<AlarmsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ALARMS_T__3214EC274414D853");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.SocketRooms).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__SOCKE__5728DECD");

            entity.HasOne(d => d.Users).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__USERS__5634BA94");

            entity.HasOne(d => d.Voc).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__VOC_I__5540965B");
        });

        modelBuilder.Entity<BuildingsTb>(entity =>
        {
            entity.HasKey(e => e.BuildingCd).HasName("PK__BUILDING__1E40968706F2C3C6");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.BuildingsTbs).HasConstraintName("FK__BUILDINGS__PLACE__34D3C6C9");
        });

        modelBuilder.Entity<EnergyMonthUsageTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ENERGY_M__3214EC276E339DB2");

            entity.Property(e => e.Apr).HasDefaultValueSql("((0))");
            entity.Property(e => e.Aug).HasDefaultValueSql("((0))");
            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Dec).HasDefaultValueSql("((0))");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Feb).HasDefaultValueSql("((0))");
            entity.Property(e => e.Jan).HasDefaultValueSql("((0))");
            entity.Property(e => e.Jul).HasDefaultValueSql("((0))");
            entity.Property(e => e.Jun).HasDefaultValueSql("((0))");
            entity.Property(e => e.Mar).HasDefaultValueSql("((0))");
            entity.Property(e => e.May).HasDefaultValueSql("((0))");
            entity.Property(e => e.Nov).HasDefaultValueSql("((0))");
            entity.Property(e => e.Oct).HasDefaultValueSql("((0))");
            entity.Property(e => e.Sep).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.MeterReader).WithMany(p => p.EnergyMonthUsageTbs).HasConstraintName("FK__ENERGY_MO__METER__19EAC663");
        });

        modelBuilder.Entity<EnergyUsagesTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ENERGY_U__3214EC2739B35F96");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<FacilitysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FACILITY__3214EC27A6C9A272");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.FacCreateDt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Rooms).WithMany(p => p.FacilitysTbs).HasConstraintName("FK__FACILITYS__ROOMS__5CE1B823");
        });

        modelBuilder.Entity<FloorsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FLOORS_T__3214EC27FD280A6C");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.FloorsTbs).HasConstraintName("FK__FLOORS_TB__BUILD__39987BE6");
        });

        modelBuilder.Entity<KakaoLogsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KAKAO_LO__3214EC27691BAC55");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MaterialsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MATERIAL__3214EC27D50AC5B9");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MeterReadersTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__METER_RE__3214EC270A243CCC");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.MeterReadersTbs).HasConstraintName("FK__METER_REA__BUILD__7D4E87B5");
        });

        modelBuilder.Entity<PlacesTb>(entity =>
        {
            entity.HasKey(e => e.PlaceCd).HasName("PK__PLACES_T__1E23CE2B54B190AD");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<RoomInventorysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOM_INV__3214EC27A3EB8F4B");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.RoomInventorysTbs).HasConstraintName("FK__ROOM_INVE__BUILD__74B941B4");

            entity.HasOne(d => d.Rooms).WithMany(p => p.RoomInventorysTbs).HasConstraintName("FK__ROOM_INVE__ROOMS__73C51D7B");
        });

        modelBuilder.Entity<RoomsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOMS_TB__3214EC270CFC2A02");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Floor).WithMany(p => p.RoomsTbs).HasConstraintName("FK__ROOMS_TB__FLOOR___3E5D3103");
        });

        modelBuilder.Entity<SocketRoomsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SOCKET_R__3214EC275805109D");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<SubItemsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SUB_ITEM__3214EC277ACAB7FA");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.SubItemsTbs).HasConstraintName("FK__SUB_ITEMS__BUILD__6D181FEC");

            entity.HasOne(d => d.Facility).WithMany(p => p.SubItemsTbs).HasConstraintName("FK__SUB_ITEMS__FACIL__6E0C4425");
        });

        modelBuilder.Entity<TotalInventorysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TOTAL_IN__3214EC2765341262");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.TotalInventorysTbs).HasConstraintName("FK__TOTAL_INV__BUILD__68536ACF");

            entity.HasOne(d => d.Material).WithMany(p => p.TotalInventorysTbs).HasConstraintName("FK__TOTAL_INV__MATER__675F4696");
        });

        modelBuilder.Entity<UnitPriceTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UNIT_PRI__3214EC2770C06535");

            entity.HasOne(d => d.EnveryUsage).WithMany(p => p.UnitPriceTbs).HasConstraintName("FK__UNIT_PRIC__ENVER__04EFA97D");

            entity.HasOne(d => d.MeterReader).WithMany(p => p.UnitPriceTbs).HasConstraintName("FK__UNIT_PRIC__METER__03FB8544");
        });

        modelBuilder.Entity<UnitTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UNIT_TB__3214EC27BC3055E1");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.UnitTbs).HasConstraintName("FK__UNIT_TB__PLACECO__09B45E9A");
        });

        modelBuilder.Entity<UsersTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS_TB__3214EC27862BD718");

            entity.Property(e => e.AdminYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.AlarmYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.UsersTbs).HasConstraintName("FK__USERS_TB__PLACEC__300F11AC");
        });

        modelBuilder.Entity<VocCommentsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VOC_COMM__3214EC27418B3A83");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Users).WithMany(p => p.VocCommentsTbs).HasConstraintName("FK__VOC_COMME__USERS__4BB72C21");

            entity.HasOne(d => d.Voc).WithMany(p => p.VocCommentsTbs).HasConstraintName("FK__VOC_COMME__VOC_I__4AC307E8");
        });

        modelBuilder.Entity<VocTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VOC_TB__3214EC27F07E102A");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.ReplyYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.VocTbs).HasConstraintName("FK__VOC_TB__BUILDING__450A2E92");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
