namespace RuleEngineService.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RuleEngineService.RuleEngineContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RuleEngineService.RuleEngineContext context)
        {
            context.People.AddOrUpdate(
                          p => p.ID,
                          new Person()
                          {
                              Name = "Jane Austen",
                              Age = 42,
                              Sex = "F",
                              Weight = 80,
                              Height = 1.6,
                              CholesterolLevel = 98,
                              Temperature = 36,
                              HasDiabetes = false,
                              HasCough = false,
                              HasBlisters = false
                          },
                          new Person()
                          {
                              Name = "Coby Williams",
                              Age = 12,
                              Sex = "M",
                              Weight = 60,
                              Height = 1.3,
                              CholesterolLevel = 80,
                              Temperature = 37,
                              HasDiabetes = false,
                              HasCough = false,
                              HasBlisters = false
                          },
                          new Person()
                          {
                              Name = "Larry Austin",
                              Age = 21,
                              Sex = "M",
                              Weight = 130,
                              Height = 1.73,
                              CholesterolLevel = 140,
                              Temperature = 36,
                              HasDiabetes = true,
                              HasCough = false,
                              HasBlisters = false
                          },
                          new Person()
                          {
                              Name = "Sophie Johnson",
                              Age = 11,
                              Sex = "F",
                              Weight = 75,
                              Height = 1.1,
                              CholesterolLevel = 98,
                              Temperature = 39,
                              HasDiabetes = false,
                              HasCough = true,
                              HasBlisters = true
                          },
                          new Person()
                          {
                              Name = "William Turner",
                              Age = 16,
                              Sex = "M",
                              Weight = 76,
                              Height = 1.8,
                              CholesterolLevel = 98,
                              Temperature = 40,
                              HasDiabetes = false,
                              HasCough = false,
                              HasBlisters = true
                          },
                          new Person()
                          {
                              Name = "Julie Madison",
                              Age = 31,
                              Sex = "F",
                              Weight = 95,
                              Height = 1.7,
                              CholesterolLevel = 114,
                              Temperature = 36,
                              HasDiabetes = true,
                              HasCough = false,
                              HasBlisters = true
                          }
                        );

            Rule r1 = new Rule()
            {
                MemberName = "Age",
                Operator = "LessThan",
                TargetValue = "12"
            };
            Rule r2 = new Rule()
            {
                MemberName = "Temperature",
                Operator = "GreaterThan",
                TargetValue = "37"
            };
            Rule r3 = new Rule()
            {
                MemberName = "HasBlisters",
                Operator = "Equal",
                TargetValue = "True"
            };


            Policy p1 = new Policy()
            {
                Name = "Chickenpox"
            };

            r1.Policies.Add(p1);
            r2.Policies.Add(p1);
            r3.Policies.Add(p1);
            p1.Rules.Add(r1);
            p1.Rules.Add(r2);
            p1.Rules.Add(r3);

            context.Database.ExecuteSqlCommand("delete from Rules");
            context.Database.ExecuteSqlCommand("delete from Policies");

            context.Rules.AddOrUpdate(r => r.ID, r1, r2, r3);
            context.Policies.AddOrUpdate(p => p.ID, p1);
        }
    }
}
