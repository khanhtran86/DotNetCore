using DotnetCoreVCB.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreVCB.Common.Database
{
    public class DemoDBContext:DbContext
    {
        public DemoDBContext(DbContextOptions<DemoDBContext> options) : base(options) { }

        public DbSet<SimpleUser> SimpleUser { get; set; }
    }
}
