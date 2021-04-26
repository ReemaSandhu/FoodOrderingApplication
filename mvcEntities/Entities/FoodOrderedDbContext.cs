using Microsoft.EntityFrameworkCore;
using mvcEntities.CustomModel;

namespace mvcEntities.Entities
{
    public partial class FoodOrderedDbContext : DbContext
    {
        public FoodOrderedDbContext()
        {
        }
        public FoodOrderedDbContext(DbContextOptions<FoodOrderedDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<MenuList> MenuList { get; set; }
        public virtual DbSet<ModeOfPayment> ModeOfPayment { get; set; }
        public virtual DbSet<OrderHistory> OrderHistory { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<CustomMenu> CustomMenu { get; set; }
        public virtual DbSet<PaymentCustom> PaymentCustom { get; set; }
        public virtual DbSet<OrderHistoryCustom> OrderHistoryCustom { get; set; }
        public virtual DbSet<PastOrders> PastOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuList>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.MenuDescription)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.MenuImage)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MenuList)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuList_Category");
            });

            modelBuilder.Entity<ModeOfPayment>(entity =>
            {
                entity.HasKey(e => e.ModeId);

                entity.Property(e => e.Mode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.TransactionId).HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderHistory_Category");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderHistory_MenuList");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderHistory_Registration");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuList_Payment");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.DateOfReservation).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Login>(entity => { });
            modelBuilder.Entity<CustomMenu>(entity => { });
            modelBuilder.Entity<PaymentCustom>(entity => { });
            modelBuilder.Entity<OrderHistoryCustom>(entity => { });
            modelBuilder.Entity<PastOrders>(entity => { });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
