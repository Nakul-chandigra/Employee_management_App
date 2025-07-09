using Microsoft.EntityFrameworkCore;

namespace practice_1.model
{
    public class DataDbcontext : DbContext
    {
        public DataDbcontext(DbContextOptions<DataDbcontext> Options) : base(Options) { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<EmployeeProject> EmployeeProject { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }

    }
}
