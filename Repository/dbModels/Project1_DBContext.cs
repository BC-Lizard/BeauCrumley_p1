using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.dbModels
{
    public partial class Project1_DBContext : DbContext
    {
        public Project1_DBContext()
        {
        }

        public Project1_DBContext(DbContextOptions<Project1_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderHistory> OrderHistories { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Project1_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.StoreNo, e.PartNo })
                    .HasName("InventoryItemNo");

                entity.ToTable("Inventory");

                entity.HasOne(d => d.PartNoNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.PartNo)
                    .HasConstraintName("FK__Inventory__PartN__412EB0B6");

                entity.HasOne(d => d.StoreNoNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreNo)
                    .HasConstraintName("FK__Inventory__Store__403A8C7D");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.PartNo)
                    .HasName("PK__Items__7C3FF6B738E12950");

                entity.Property(e => e.PartDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PartImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PartName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PartPrice).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.PartSale).HasColumnType("decimal(4, 2)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderNo)
                    .HasName("PK__Orders__C3907C74881B535A");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Tax).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.AccountNoNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AccountNo)
                    .HasConstraintName("FK__Orders__AccountN__44FF419A");

                entity.HasOne(d => d.StoreNoNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreNo)
                    .HasConstraintName("FK__Orders__StoreNo__440B1D61");
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => new { e.OrderNo, e.PartNo })
                    .HasName("OrderHistoryNo");

                entity.ToTable("OrderHistory");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.OrderNoNavigation)
                    .WithMany(p => p.OrderHistories)
                    .HasForeignKey(d => d.OrderNo)
                    .HasConstraintName("FK__OrderHist__Order__47DBAE45");

                entity.HasOne(d => d.PartNoNavigation)
                    .WithMany(p => p.OrderHistories)
                    .HasForeignKey(d => d.PartNo)
                    .HasConstraintName("FK__OrderHist__PartN__48CFD27E");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateNo)
                    .HasName("PK__States__C3BA13D9B0F771A4");

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxRate).HasColumnType("decimal(4, 2)");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreNo)
                    .HasName("PK__Stores__3B82886E8710B77A");

                entity.Property(e => e.StoreCity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StoreStreetAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.StoreStateNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreState)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Stores__StoreSta__38996AB5");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AccountNo)
                    .HasName("PK__Users__349D9DFDAAEE4A74");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DefaultStoreNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DefaultStore)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Users__DefaultSt__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
