namespace RuleEngineService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.String(maxLength: 1),
                        Weight = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        CholesterolLevel = c.Int(nullable: false),
                        Temperature = c.Int(nullable: false),
                        HasDiabetes = c.Boolean(nullable: false),
                        HasCough = c.Boolean(nullable: false),
                        HasBlisters = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Policies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberName = c.String(),
                        Operator = c.String(),
                        TargetValue = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RulePolicies",
                c => new
                    {
                        Rule_ID = c.Int(nullable: false),
                        Policy_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rule_ID, t.Policy_ID })
                .ForeignKey("dbo.Rules", t => t.Rule_ID, cascadeDelete: true)
                .ForeignKey("dbo.Policies", t => t.Policy_ID, cascadeDelete: true)
                .Index(t => t.Rule_ID)
                .Index(t => t.Policy_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RulePolicies", "Policy_ID", "dbo.Policies");
            DropForeignKey("dbo.RulePolicies", "Rule_ID", "dbo.Rules");
            DropIndex("dbo.RulePolicies", new[] { "Policy_ID" });
            DropIndex("dbo.RulePolicies", new[] { "Rule_ID" });
            DropTable("dbo.RulePolicies");
            DropTable("dbo.Rules");
            DropTable("dbo.Policies");
            DropTable("dbo.People");
        }
    }
}
