using _01_EFC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace _01_EFC.Contexts;

internal class DataContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SaadFatih\Datalagring\Submission_Task_Master\01_EFC\Contexts\sql_db_task.mdf;Integrated Security=True;Connect Timeout=30";



    #region constructors
    public DataContext()
    {
        
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }


    #endregion


    #region overrides
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }


    #endregion


    public DbSet<CustomerEntity> Customers { get; set; } = null!;
    public DbSet<StatusEntity> Status { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
    public DbSet<TicketEntity> Tickets { get; set; } = null!;



}
