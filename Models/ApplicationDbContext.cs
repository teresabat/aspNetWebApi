using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Pessoa> Pessoas {get;set;}
    }
}