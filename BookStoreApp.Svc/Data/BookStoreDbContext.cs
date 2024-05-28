using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Svc.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
  public BookStoreDbContext() { }

  public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

  public virtual DbSet<Author> Authors { get; set; }
  public virtual DbSet<Book> Books { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Author>(entity =>
    {
      entity.Property(e => e.FirstName).HasMaxLength(50);
      entity.Property(e => e.LastName).HasMaxLength(50);
      entity.Property(e => e.Bio).HasMaxLength(250);
    });

    modelBuilder.Entity<Book>(entity =>
    {
      entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EAA1D0DE41").IsUnique();

      entity.Property(e => e.Image).HasMaxLength(50);
      entity.Property(e => e.Isbn).HasMaxLength(50).HasColumnName("ISBN");
      entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.Summary).HasMaxLength(250);
      entity.Property(e => e.Title).HasMaxLength(50);

      entity.HasOne(d => d.Author)
        .WithMany(p => p.Books)
        .HasForeignKey(d => d.AuthorId)
        .HasConstraintName("FK_Books_Authors");
    });

    modelBuilder.Entity<IdentityRole>()
      .HasData(
        new IdentityRole
        {
          Id = "739ba9cd-38ff-487c-b788-d9474bb8f2c1",
          Name = "Administrator",
          NormalizedName = "ADMINSTRATOR",
        },
        new IdentityRole
        {
          Id = "c9c5a700-cf36-48b2-82c8-3e38a969f1fd",
          Name = "User",
          NormalizedName = "USER",
        }        
      );

    var hasher = new PasswordHasher<ApiUser>();

    modelBuilder.Entity<ApiUser>()
      .HasData(
        new ApiUser 
        {
          Id = "0a79c58f-a4ec-44b6-954c-1076c76f8071",
          FirstName = "System",
          LastName = "Admin",
          Email = "admin@bookstor.com",
          NormalizedEmail = "ADMIN@BOOKSTORE.COM",
          UserName = "admin@bookstor.com",
          NormalizedUserName = "ADMIN@BOOKSTORE.COM",
          PasswordHash = hasher.HashPassword(null, "P@ssword1")
        },
        new ApiUser 
        { 
          Id = "b2ef7b52-0284-43d5-9b6c-64a25a04e53d",
          FirstName = "System",
          LastName = "User",
          Email = "user@bookstor.com",
          NormalizedEmail = "USER@BOOKSTORE.COM",
          UserName = "user@bookstor.com",
          NormalizedUserName = "USER@BOOKSTORE.COM",
          PasswordHash = hasher.HashPassword(null, "P@ssword1")
        }
      );

    modelBuilder.Entity<IdentityUserRole<string>>()
      .HasData(
        new IdentityUserRole<string> 
        {
          RoleId = "739ba9cd-38ff-487c-b788-d9474bb8f2c1",
          UserId = "0a79c58f-a4ec-44b6-954c-1076c76f8071"
        },
        new IdentityUserRole<string>
        {
          RoleId = "c9c5a700-cf36-48b2-82c8-3e38a969f1fd",
          UserId = "b2ef7b52-0284-43d5-9b6c-64a25a04e53d"
        }
      );

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
