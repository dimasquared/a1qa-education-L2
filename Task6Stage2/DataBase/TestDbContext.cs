using Microsoft.EntityFrameworkCore;
using Task6Stage2.DataBase.Models;
using Task6Stage2.DataBase.Utils;

namespace Task6Stage2.DataBase;

public class TestDbContext : DbContext
{
    public DbSet<Test> Test { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<Session> Session { get; set; }
    public DbSet<Author> Author { get; set; }

    public TestDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(DbConfigUtil.GetConnectionString(), new MySqlServerVersion(new Version(8, 0, 30)));
    }
}