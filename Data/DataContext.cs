using Microsoft.EntityFrameworkCore;
using SQL_Project.Models;

namespace SQL_Project.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){ }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }  
    }
}
