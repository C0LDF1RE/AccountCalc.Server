using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountCalc.Server.Data
{
    public class AccountDbContext:DbContext
    {
        public AccountDbContext(DbContextOptions options) : base(options) { }
        public DbSet<AccountModel> account { get; set; }
    }
}
