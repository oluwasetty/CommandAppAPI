using ApprovalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApprovalApp.Data
{
    public class ApprovalAppContext : DbContext
    {
        public ApprovalAppContext(DbContextOptions<ApprovalAppContext> opt) : base(opt)
        {

        }

        public DbSet<Command> Commands { get; set; }

    }
}