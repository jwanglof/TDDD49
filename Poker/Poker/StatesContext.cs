using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Poker
{
    public class StatesContext : DbContext
    {
        public StatesContext() { }
        public DbSet<States> States { get; set; }
    }
}
