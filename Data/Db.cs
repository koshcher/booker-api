using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class Db : IdentityDbContext<User>
{
    public Db(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var books = builder.Entity<Book>();
        books.HasKey(x => x.Id);

        var users = builder.Entity<User>();
        users.HasKey(x => x.Id);
    }
}