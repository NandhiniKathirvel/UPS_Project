using EmployeeDetailsWebAPI.Context;
using EmployeeDetailsWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeDetailsWebAPI.Context
{
    public class CurdContext : DbContext
    {
        public CurdContext(DbContextOptions<CurdContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}




