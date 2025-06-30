namespace LLM_Interaction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProviderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.Prompts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Tags = c.String(),
                        ModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .Index(t => t.ModelId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ApiUrl = c.String(),
                        ApiKey = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Models", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Prompts", "ModelId", "dbo.Models");
            DropIndex("dbo.Prompts", new[] { "ModelId" });
            DropIndex("dbo.Models", new[] { "ProviderId" });
            DropTable("dbo.Providers");
            DropTable("dbo.Prompts");
            DropTable("dbo.Models");
        }
    }
}
