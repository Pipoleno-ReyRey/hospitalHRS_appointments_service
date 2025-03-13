using Microsoft.EntityFrameworkCore;

public class UsersDB : DbContext
{
    public DbSet<Users> users { get; set; }
    public UsersDB(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Users>().HasIndex(u => u.insurance_num).IsUnique();
        modelBuilder.Entity<Users>().HasIndex(u => u.email).IsUnique();
    }
}