using System;
using System.Collections.Generic;
using FamTec.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        => optionsBuilder.UseSqlServer("Server=123.2.156.122,1002;Database=FMS;User Id=stec;Password=stecdev1234!;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminPlacesTb>(entity =>
        {
            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany().HasConstraintName("FK__ADMIN_PLA__PLACE__5C57A83E");

            entity.HasOne(d => d.UsersUser).WithMany().HasConstraintName("FK__ADMIN_PLA__USERS__5B638405");
        });

        modelBuilder.Entity<AdminsTb>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__ADMINS_T__F3BEEBFF907812AA");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<AlarmsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ALARMS_T__3214EC27BE0FBC82");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.SocketRooms).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__SOCKE__11007AA7");

            entity.HasOne(d => d.UsersUser).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__USERS__100C566E");

            entity.HasOne(d => d.Voc).WithMany(p => p.AlarmsTbs).HasConstraintName("FK__ALARMS_TB__VOC_I__0F183235");
        });

        modelBuilder.Entity<BuildingsTb>(entity =>
        {
            entity.HasKey(e => e.BuildingCd).HasName("PK__BUILDING__1E4096878D4E881A");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.BuildingsTbs).HasConstraintName("FK__BUILDINGS__PLACE__6EAB62A3");
        });

        modelBuilder.Entity<EnergyMonthUsageTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ENERGY_M__3214EC279CB248BE");

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

            entity.HasOne(d => d.MeterReader).WithMany(p => p.EnergyMonthUsageTbs).HasConstraintName("FK__ENERGY_MO__METER__53C2623D");
        });

        modelBuilder.Entity<EnergyUsagesTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ENERGY_U__3214EC27E62B0CD4");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<FacilitysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FACILITY__3214EC276E6EB7C2");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.FacCreateDt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Rooms).WithMany(p => p.FacilitysTbs).HasConstraintName("FK__FACILITYS__ROOMS__16B953FD");
        });

        modelBuilder.Entity<FloorsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FLOORS_T__3214EC27FE145683");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.FloorsTbs).HasConstraintName("FK__FLOORS_TB__BUILD__737017C0");
        });

        modelBuilder.Entity<KakaoLogsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KAKAO_LO__3214EC275FE7BB7E");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MaterialsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MATERIAL__3214EC27BC6817A3");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<MeterReadersTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__METER_RE__3214EC27F053E712");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.MeterReadersTbs).HasConstraintName("FK__METER_REA__BUILD__3726238F");
        });

        modelBuilder.Entity<PlacesTb>(entity =>
        {
            entity.HasKey(e => e.PlaceCd).HasName("PK__PLACES_T__1E23CE2B64DCA2DE");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            //entity.Property(u => u.Id).ValueGeneratedOnUpdate().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            //entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<RoomInventorysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOM_INV__3214EC27E344F7B0");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.RoomInventorysTbs).HasConstraintName("FK__ROOM_INVE__BUILD__2E90DD8E");

            entity.HasOne(d => d.Rooms).WithMany(p => p.RoomInventorysTbs).HasConstraintName("FK__ROOM_INVE__ROOMS__2D9CB955");
        });

        modelBuilder.Entity<RoomsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROOMS_TB__3214EC27F5AB4804");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            

            entity.HasOne(d => d.Floor).WithMany(p => p.RoomsTbs).HasConstraintName("FK__ROOMS_TB__FLOOR___7834CCDD");
        });

        modelBuilder.Entity<SocketRoomsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SOCKET_R__3214EC27F1086845");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<SubItemsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SUB_ITEM__3214EC27353E4B4E");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.SubItemsTbs).HasConstraintName("FK__SUB_ITEMS__BUILD__26EFBBC6");

            entity.HasOne(d => d.Facility).WithMany(p => p.SubItemsTbs).HasConstraintName("FK__SUB_ITEMS__FACIL__27E3DFFF");
        });

        modelBuilder.Entity<TotalInventorysTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TOTAL_IN__3214EC2701903AE7");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.TotalInventorysTbs).HasConstraintName("FK__TOTAL_INV__BUILD__222B06A9");

            entity.HasOne(d => d.Material).WithMany(p => p.TotalInventorysTbs).HasConstraintName("FK__TOTAL_INV__MATER__2136E270");
        });

        modelBuilder.Entity<UnitPriceTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UNIT_PRI__3214EC273B4A2466");

            entity.HasOne(d => d.EnveryUsage).WithMany(p => p.UnitPriceTbs).HasConstraintName("FK__UNIT_PRIC__ENVER__3EC74557");

            entity.HasOne(d => d.MeterReader).WithMany(p => p.UnitPriceTbs).HasConstraintName("FK__UNIT_PRIC__METER__3DD3211E");
        });

        modelBuilder.Entity<UnitTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UNIT_TB__3214EC2700B6DA04");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.UnitTbs).HasConstraintName("FK__UNIT_TB__PLACECO__438BFA74");
        });

        modelBuilder.Entity<UsersTb>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERS_TB__F3BEEBFFE548CE30");

            entity.Property(e => e.AdminYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.AlarmYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.PlacecodeCdNavigation).WithMany(p => p.UsersTbs).HasConstraintName("FK__USERS_TB__PLACEC__69E6AD86");
        });

        modelBuilder.Entity<VocCommentsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VOC_COMM__3214EC276FAC1EA3");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.UsersUser).WithMany(p => p.VocCommentsTbs).HasConstraintName("FK__VOC_COMME__USERS__058EC7FB");

            entity.HasOne(d => d.Voc).WithMany(p => p.VocCommentsTbs).HasConstraintName("FK__VOC_COMME__VOC_I__049AA3C2");
        });

        modelBuilder.Entity<VocTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VOC_TB__3214EC271F894171");

            entity.Property(e => e.CreateDt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.ReplyYn).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.BuildingCdNavigation).WithMany(p => p.VocTbs).HasConstraintName("FK__VOC_TB__BUILDING__7EE1CA6C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
