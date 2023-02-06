using Microsoft.EntityFrameworkCore;

namespace AutoRepair.Domain;

public partial class AutoRepairContext : DbContext
{
    public AutoRepairContext()
    {
    }

    public AutoRepairContext(DbContextOptions<AutoRepairContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Limousine> Limousines { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Server=DESKTOP-88G2M6G;Database=AutoRepair;Trusted_Connection=True;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC0722CA9FBF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CarId).HasColumnName("Car_Id");
            entity.Property(e => e.From).HasColumnType("datetime");
            entity.Property(e => e.To).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(entity => entity.IsOnTheRoad);
        });

        //entity.Property(entity => entity.Genre).HasConversion(entity => Convert.ToInt32(entity), dbEntity => (Genre)dbEntity);

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(entity => entity.Brand).HasConversion(entity => Convert.ToInt32(entity), dbEntity => (Brand)dbEntity);
            entity.Property(entity => entity.Color).HasConversion(entity => Convert.ToInt32(entity), dbEntity => (Color)dbEntity);
            entity.Property(entity => entity.IsCarBooked);
            entity.Property(e => e.Year).HasColumnType("date");
        });

        modelBuilder.Entity<Limousine>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(entity => entity.Bar);
            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Limousine).HasForeignKey<Limousine>(d => d.Id);
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TrunkSize);
            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Truck).HasForeignKey<Truck>(d => d.Id);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0747153E7E");
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
