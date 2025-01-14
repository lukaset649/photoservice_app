using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using photoservice.Models;

namespace photoservice.Data;

public partial class PhotoserviceContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PhotoserviceContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public PhotoserviceContext(DbContextOptions<PhotoserviceContext> options, IConfiguration configuration)
           : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<ActiveUsersWithRole> ActiveUsersWithRoles { get; set; }

    public virtual DbSet<DetailsBaptism> DetailsBaptisms { get; set; }

    public virtual DbSet<DetailsOther> DetailsOthers { get; set; }

    public virtual DbSet<DetailsPhotoshoot> DetailsPhotoshoots { get; set; }

    public virtual DbSet<DetailsWedding> DetailsWeddings { get; set; }

    public virtual DbSet<EmployeeReservationsView> EmployeeReservationsViews { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<EquipmentCompability> EquipmentCompabilities { get; set; }

    public virtual DbSet<EquipmentConditionLog> EquipmentConditionLogs { get; set; }

    public virtual DbSet<EquipmentManufacturer> EquipmentManufacturers { get; set; }

    public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationCancellation> ReservationCancellations { get; set; }

    public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }

    public virtual DbSet<ReservationEmployee> ReservationEmployees { get; set; }

    public virtual DbSet<ReservationEquipment> ReservationEquipments { get; set; }

    public virtual DbSet<ReservationType> ReservationTypes { get; set; }

    public virtual DbSet<ReservationView> ReservationViews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveUsersWithRole>(entity =>
        {
            entity.ToView("ActiveUsersWithRoles");
        });

        modelBuilder.Entity<DetailsBaptism>(entity =>
        {
            entity.HasKey(e => e.IdDetBap).HasName("PK__details___30B886D24C52BFFB");
        });

        modelBuilder.Entity<DetailsOther>(entity =>
        {
            entity.HasKey(e => e.IdDetOth).HasName("PK__details___3FC1B3BFC7ECE7AF");
        });

        modelBuilder.Entity<DetailsPhotoshoot>(entity =>
        {
            entity.HasKey(e => e.IdDetPs).HasName("PK__details___E90F3DEC3277CA3D");
        });

        modelBuilder.Entity<DetailsWedding>(entity =>
        {
            entity.HasKey(e => e.IdDetWed).HasName("PK__details___3DC18B8372A5F2EB");
        });

        modelBuilder.Entity<EmployeeReservationsView>(entity =>
        {
            entity.ToView("EmployeeReservationsView");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.IdEq).HasName("PK__equipmen__00B7CEB769FDCDDB");

            entity.ToTable("equipment", tb => tb.HasTrigger("LogEquipmentConditionChange"));

            entity.Property(e => e.Working).HasDefaultValue(true);

            entity.HasOne(d => d.EqManufacturer).WithMany(p => p.Equipment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__equipment__eq_ma__5BE2A6F2");

            entity.HasOne(d => d.EqType).WithMany(p => p.Equipment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__equipment__eq_ty__5AEE82B9");
        });

        modelBuilder.Entity<EquipmentCompability>(entity =>
        {
            entity.HasKey(e => e.IdCompability).HasName("PK__equipmen__2D7250C1DD452A99");

            entity.HasOne(d => d.CompatibleWith).WithMany(p => p.EquipmentCompabilityCompatibleWiths)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__equipment__compa__5FB337D6");

            entity.HasOne(d => d.Eq).WithMany(p => p.EquipmentCompabilityEqs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__equipment__eq_id__5EBF139D");
        });

        modelBuilder.Entity<EquipmentConditionLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__equipmen__9E2397E0AF719301");
        });

        modelBuilder.Entity<EquipmentManufacturer>(entity =>
        {
            entity.HasKey(e => e.IdEqMan).HasName("PK__equipmen__68D204E80E29EA6B");
        });

        modelBuilder.Entity<EquipmentType>(entity =>
        {
            entity.HasKey(e => e.IdEqType).HasName("PK__equipmen__90878FF3A8328C5F");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.IdMess).HasName("PK__messages__68A1E1B79CFF28B9");

            entity.Property(e => e.MessDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Recipient).WithMany(p => p.MessageRecipients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__recipi__3F466844");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__sender__3E52440B");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdRes).HasName("PK__reservat__6ABE6F787E5F6FA4");

            entity.Property(e => e.ReservationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StatusId).HasDefaultValue(1);

            entity.HasOne(d => d.Client).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__clien__4D94879B");

            entity.HasOne(d => d.Service).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__servi__4E88ABD4");

            entity.HasOne(d => d.Status).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__statu__4F7CD00D");
        });

        modelBuilder.Entity<ReservationCancellation>(entity =>
        {
            entity.HasKey(e => e.IdCancell).HasName("PK__reservat__AD54E76A4522B051");

            entity.Property(e => e.CancellDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CancelledByNavigation).WithMany(p => p.ReservationCancellations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__cance__75A278F5");

            entity.HasOne(d => d.Res).WithMany(p => p.ReservationCancellations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__res_i__74AE54BC");
        });

        modelBuilder.Entity<ReservationDetail>(entity =>
        {
            entity.HasKey(e => e.IdResDet).HasName("PK__reservat__5CC3245C8EF51E8B");

            entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__reser__6FE99F9F");

            entity.HasOne(d => d.Type).WithMany(p => p.ReservationDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__type___70DDC3D8");
        });

        modelBuilder.Entity<ReservationEmployee>(entity =>
        {
            entity.HasKey(e => e.IdResEmp).HasName("PK__reservat__5C7F75F1BD96BEF3");

            entity.HasOne(d => d.Employee).WithMany(p => p.ReservationEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__emplo__534D60F1");

            entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__reser__52593CB8");
        });

        modelBuilder.Entity<ReservationEquipment>(entity =>
        {
            entity.HasKey(e => e.IdResEq).HasName("PK__reservat__1942D268483C10A9");

            entity.HasOne(d => d.Eq).WithMany(p => p.ReservationEquipments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__eq_id__6383C8BA");

            entity.HasOne(d => d.Res).WithMany(p => p.ReservationEquipments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reservati__res_i__628FA481");
        });

        modelBuilder.Entity<ReservationType>(entity =>
        {
            entity.HasKey(e => e.IdResType).HasName("PK__reservat__FF0D841BAB47F154");
        });

        modelBuilder.Entity<ReservationView>(entity =>
        {
            entity.ToView("ReservationView");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__roles__3D48441DC0B498CA");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PK__service___D06FB5A8A81C6216");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__status__5D2DC6E8F31257AD");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__users__D2D1463775921919");

            entity.ToTable("users", tb => tb.HasTrigger("PreventUserDeletion"));

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.IdUr).HasName("PK__user_rol__014848A48D79EEF6");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasConstraintName("FK__user_role__role___44FF419A");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasConstraintName("FK__user_role__user___440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
