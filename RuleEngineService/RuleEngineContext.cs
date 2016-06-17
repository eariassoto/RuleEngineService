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
        public RuleEngineContext() : base("Server=tcp:ruleengineserver.database.windows.net,1433;Data Source=ruleengineserver.database.windows.net;Initial Catalog=RuleEngineDB;Persist Security Info=False;User ID=ruleengine;Password=cafeCR12;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;") { }
        public DbSet<Person> People { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Policy> Policies { get; set; }
    }
}
