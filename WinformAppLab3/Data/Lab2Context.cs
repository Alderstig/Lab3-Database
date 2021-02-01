using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.IO;

#nullable disable

namespace WinformsAppLab3
{
    public partial class Lab2Context : DbContext
    {
        public Lab2Context()
        {
        }

        public Lab2Context(DbContextOptions<Lab2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<StockBalance> StockBalances { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets("2f7981df-188d-4848-af2c-118ca3179291")
                    .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(13)
                    .HasColumnName("ID");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_Authors");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Books_Genres");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.City1)
                    .HasMaxLength(50)
                    .HasColumnName("City");

                entity.Property(e => e.Country).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HomePhone).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(6);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Employees_Cities");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Employees_Stores");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Genre1)
                    .HasMaxLength(20)
                    .HasColumnName("Genre");
            });

            modelBuilder.Entity<StockBalance>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.Isbn13 });

                entity.ToTable("StockBalance");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Isbn13)
                    .HasMaxLength(13)
                    .HasColumnName("ISBN13");

                entity.HasOne(d => d.Isbn13Navigation)
                    .WithMany(p => p.StockBalances)
                    .HasForeignKey(d => d.Isbn13)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockBalance_Books");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StockBalances)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StockBalance_Stores");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.StoreName).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Stores_Cities");
            });

            modelBuilder.Entity<TitlesPerAuthor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TitlesPerAuthor");

                entity.Property(e => e.Name).HasMaxLength(101);

                entity.Property(e => e.StockValueKr).HasColumnName("StockValue (Kr)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
