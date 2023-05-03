using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using StockManagamentSystem.Modules.MySQL;

public class DBContext : DbContext
{
    private static string connString = "Server=dmrcogluomer.kodlama.net.tr;Database=stock_follow;Uid=dmrcogluomer;Pwd=!DMRCOGLUOMR61?;";
    public DbSet<Users> Users { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Stocks> Stocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseMySql(connString);
    }

    private static DBContext _instance;

    public static DBContext Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DBContext();
            }
            return _instance;
        }
    }


}