using RuleEngineService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngineService
{
    public class RuleEngineContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Policy> Policies { get; set; }
    }
}
