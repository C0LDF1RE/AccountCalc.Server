using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCalc.Server.Data
{
    public class TestDbContext:DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options) { }
        public DbSet<TestModel> User_Login { get; set; }
    }
}
